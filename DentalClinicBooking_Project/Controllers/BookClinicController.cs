using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using DentalClinicBooking_Project.Models.ViewModels.ViewScheduleModels;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using PayPal.Api;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace DentalClinicBooking_Project.Controllers
{
    public class BookClinicController : Controller
    {
		public JsonSerializerSettings settings = new JsonSerializerSettings
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		};//cấu hình JsonSerializerSettings để bỏ qua các vòng lặp tham chiếu:
         
		private readonly IClinicRepository clinicRepository;
        private readonly IClinicAppointmentScheduleRepository clinicAppointmentScheduleRepository;
        private readonly ISlotRepository slotRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IHttpContextAccessor _contx;
        private readonly IBasicRepository basicRepository;
        private readonly IServiceRepository serviceRepository;

        public BookClinicController(IClinicRepository bookClinicRepository,
            IClinicAppointmentScheduleRepository clinicAppointmentScheduleRepository,
            ISlotRepository slotRepository,
            IPatientRepository patientRepository,
            IHttpContextAccessor contx,
            IBasicRepository basicRepository,
            IServiceRepository serviceRepository)
        {
            this.clinicRepository = bookClinicRepository;
            this.clinicAppointmentScheduleRepository = clinicAppointmentScheduleRepository;
            this.slotRepository = slotRepository;
            this.patientRepository = patientRepository;
            _contx = contx;
            this.basicRepository = basicRepository;
            this.serviceRepository = serviceRepository;
        }


        [HttpGet]
        public async Task<IActionResult> AllClinics(
            string? searchQuery,
            int pageSize = 2,
            int pageNumber = 1)
        {
            var totalRecords = await clinicRepository.CountAsync(searchQuery);
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);//bao nhiêu trang

            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.PageSize = pageSize;//số phần tử mỗi trang
            ViewBag.PageNumber = pageNumber;//trang bao nhiêu

            var Model = await clinicRepository.GetAllAsync(searchQuery, pageNumber, pageSize);

            return View(Model);
        }

        [HttpGet]
        public async Task<IActionResult> ClinicDetails(Guid id)
        {
            var clinic = await clinicRepository.GetAsync(id);

            if (clinic != null)
            {
                var Model = new ShowClinicDetails
                {
                    Id = clinic.ClinicId,
                    ClinicName = clinic.ClinicName,
                    MainImage = clinic.MainImage,
                    Description = clinic.Description,
                    ClinicImages = clinic.ClinicImages,
                    SlotOfClinics = clinic.SlotOfClinics,
                };

                return View(Model);
            }

            return RedirectToAction("AllClinics");
        }

        [HttpGet]
        public async Task<IActionResult> BookingClinic(Guid id)
        {
            var clinic = await clinicRepository.GetAsync(id);
            var model = new ClinicBookingDisplay
            {
                ClinicId = clinic?.ClinicId ?? Guid.Empty,
                ClinicName = clinic?.ClinicName ?? string.Empty,
                MainImage = clinic?.MainImage ?? string.Empty,
                Basics = clinic?.Basics ?? Enumerable.Empty<Basic>(),
                SlotOfClinics = clinic?.SlotOfClinics ?? Enumerable.Empty<SlotOfClinic>(),
                Services = clinic?.Services ?? Enumerable.Empty<Service>(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckSlots([FromBody] BookingInfo bookingModel)
        {
            bookingModel ??= new BookingInfo();

            var bookings = await clinicAppointmentScheduleRepository
                .GetSlotAsync(
                bookingModel.date,
                bookingModel.clinicId,
                bookingModel.basicId)
                ?? new List<BookingSlot>();

            var list = await slotRepository.GetAllSlotsAsync(bookingModel.clinicId);

            var slots = new Dictionary<Guid, SlotInfo>();
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

            foreach (var item in list)
            {
                slots[item.SlotId] = new SlotInfo
                {
                    Count = 0,
                    DateTime = item.StartTime.ToString("HH:mm") + "-" + bookingModel.date.ToString("yyyy-MM-dd")
                };
            }

            foreach (var booking in bookings)
            {
                if (slots.ContainsKey(booking?.SlotId ?? Guid.Empty))
                {
                    var currentSlot = slots[booking?.SlotId ?? Guid.Empty];
                    currentSlot.Count = booking.Count;
                }
            }

            return Ok(slots);
        }

		[HttpPost]
		public async Task<IActionResult> AppointmentBookingInfo([FromBody] AppointmentBookingViewModel appointmentBookingModel)
		{
			string? patientString = _contx.HttpContext?.Session.GetString("patient");
			if (!string.IsNullOrEmpty(patientString))
			{
				var patient = JsonConvert.DeserializeObject<Patient>(patientString);
				ClinicAppointmentSchedule clinicAppointmentSchedule = new ClinicAppointmentSchedule
				{
					Code = AppointmentBookingViewModel.BookingCode(),
					PatientId = patient!.PatientId,
					ClinicId = appointmentBookingModel.ClinicId,
					BasicId = appointmentBookingModel?.BasicId,
					Date = appointmentBookingModel?.Date ?? DateOnly.FromDateTime(DateTime.Now),
					SlotId = appointmentBookingModel?.SlotId,
					ServiceId = appointmentBookingModel?.ServiceId,
					Type = "Book Appointment",
					Status = false
				};

				var model = await clinicAppointmentScheduleRepository.GetDuplicateAsync(clinicAppointmentSchedule);
				if (model == null)
				{
					// Lưu thông tin lịch hẹn vào session để xử lý sau khi thanh toán thành công
					_contx.HttpContext.Session.SetString("clinicAppointmentSchedule", JsonConvert.SerializeObject(clinicAppointmentSchedule));
					// Chuyển hướng người dùng đến phương thức PaymentWithPaypal
					return Ok(new
					{
						redirectUrl = Url.Action("PaymentWithPaypal"),
						success = true
					});
				}
				else
				{
					return Ok(new { success = false });
				}
			}
			return RedirectToAction("Login", "Login");
		}

		[HttpGet]
        public async Task<IActionResult> ConfirmBooking(Guid id)
        {
            string? patientString = _contx.HttpContext?.Session.GetString("patient");
            if (!string.IsNullOrEmpty(patientString))
            {
                var patient = JsonConvert.DeserializeObject<Patient>(patientString);
                var appointmentSchedule = await clinicAppointmentScheduleRepository.GetAsync(id);
                var clinic = await clinicRepository.GetAsync(appointmentSchedule?.ClinicId ?? Guid.Empty);
                var basic = await basicRepository.GetAsync(appointmentSchedule?.BasicId ?? Guid.Empty);
                var slot = await slotRepository.GetAsync(
                    appointmentSchedule?.ClinicId ?? Guid.Empty,
                    appointmentSchedule?.SlotId ?? Guid.Empty);
                var service = await serviceRepository.GetAsync(appointmentSchedule?.ServiceId ?? Guid.Empty);

                var model = new AppointmentBookingSuccess
                {
                    ClinicName = clinic?.ClinicName,
                    MainImage = clinic?.MainImage,
                    basicAddress = basic?.Address,
                    Code = appointmentSchedule?.Code,
                    Date = appointmentSchedule?.Date ?? DateOnly.FromDateTime(DateTime.Now),
                    SlotOfClinics = slot,
                    BasicName = basic?.BasicName,
                    Service = service?.ServiceName,
                    PatientName = patient?.PatientName,
                    BirthDate = patient?.BirthDay,
                    Gender = AppointmentBookingSuccess.GetGender(patient?.Gender),
                    PatientAddress = patient?.Address,
                    Status = DisplaySchedule.GetStatus(appointmentSchedule?.Status)
                };
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }



        public IActionResult FailureView()
        {
            return View();
        }

        public IActionResult SuccessView()
        {
            return View();
        }

		//Payment with Paypal
		[HttpGet]
		public async Task<IActionResult> PaymentWithPaypal(string Cancel = null)
		{
			// Lấy APIContext từ PaypalConfiguration
			APIContext apiContext = PaypalConfiguration.GetAPIContext();
			try
			{
				// Đại diện cho người thanh toán với phương thức thanh toán là PayPal
				// Payer Id sẽ được trả về khi thanh toán tiến hành hoặc khi nhấn thanh toán
				string payerId = Request.Query["PayerID"].ToString();

				// Kiểm tra nếu payerId là null hoặc rỗng
				if (string.IsNullOrEmpty(payerId))
				{
					// Phần này sẽ được thực thi đầu tiên vì PayerID không tồn tại
					// PayerID được trả về bởi lệnh gọi hàm create của lớp payment
					// Tạo một thanh toán mới
					// baseURL là URL mà PayPal gửi lại dữ liệu
					string baseURI = $"{Request.Scheme}://{Request.Host}/bookclinic/PaymentWithPayPal?";

					// Tạo một GUID để lưu trữ paymentID nhận được trong session
					// sẽ được sử dụng trong quá trình thực hiện thanh toán
					var guid = Convert.ToString((new Random()).Next(100000));

					// Hàm CreatePayment trả về URL phê duyệt thanh toán
					// người thanh toán sẽ được chuyển hướng tới URL này để thanh toán bằng PayPal
					var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

					// Lấy các liên kết trả về từ PayPal trong phản hồi của lệnh gọi hàm Create
					var links = createdPayment.links.GetEnumerator();
					string paypalRedirectUrl = null;

					// Duyệt qua các liên kết
					while (links.MoveNext())
					{
						Links lnk = links.Current;
						if (lnk.rel.ToLower().Trim().Equals("approval_url"))
						{
							// Lưu URL chuyển hướng của PayPal để người dùng được chuyển hướng đến trang thanh toán của PayPal
							paypalRedirectUrl = lnk.href;
						}
					}

					// Lưu paymentID vào khóa guid trong session
					_contx.HttpContext.Session.SetString(guid, createdPayment.id);

					// Chuyển hướng người dùng tới URL phê duyệt thanh toán của PayPal
					return Redirect(paypalRedirectUrl);
				}
				else
				{
					// Phần này được thực thi sau khi nhận tất cả các tham số cho thanh toán
					var guid = Request.Query["guid"].ToString();
					var paymentId = _contx.HttpContext.Session.GetString(guid);
					var executedPayment = ExecutePayment(apiContext, payerId, paymentId);

					// Nếu thanh toán thất bại thì sẽ hiển thị thông báo thất bại cho người dùng
					if (executedPayment.state.ToLower() != "approved")
					{
						_contx.HttpContext.Session.Remove("clinicAppointmentSchedule");
						return View("FailureView");
					}
				}
			}
			catch (Exception ex)
			{
				// Nếu có lỗi, hiển thị thông báo thất bại
				_contx.HttpContext.Session.Remove("clinicAppointmentSchedule");
				return View("FailureView");
			}

			// Nếu thanh toán thành công
			string? clinicAppointmentScheduleString = _contx.HttpContext?.Session.GetString("clinicAppointmentSchedule");
			if (clinicAppointmentScheduleString != null)
			{
				var clinicAppointmentSchedule = JsonConvert.DeserializeObject<ClinicAppointmentSchedule>(clinicAppointmentScheduleString);
				var model = await clinicAppointmentScheduleRepository.AddAsync(clinicAppointmentSchedule);

				// Xóa thông tin lịch hẹn khỏi session sau khi đã thêm vào cơ sở dữ liệu
				_contx.HttpContext.Session.Remove("clinicAppointmentSchedule");

				return RedirectToAction("ConfirmBooking", new { id = model.ClinicAppointmentScheduleId });
			}
			return RedirectToAction("HomePage", "Home");
		}

		private PayPal.Api.Payment payment;
		private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
		{
			var paymentExecution = new PaymentExecution()
			{
				payer_id = payerId
			};
			this.payment = new Payment()
			{
				id = paymentId
			};
			return this.payment.Execute(apiContext, paymentExecution);
		}
		private Payment CreatePayment(APIContext apiContext, string redirectUrl)
		{
			//create itemlist and add item objects to it  
			var itemList = new ItemList()
			{
				items = new List<Item>()
			};
			//Adding Item Details like name, currency, price etc  
			itemList.items.Add(new Item()
			{
				name = "Item Name comes here",
				currency = "USD",
				price = "1",
				quantity = "1",
				sku = "sku"
			});
			var payer = new Payer()
			{
				payment_method = "paypal"
			};
			// Configure Redirect Urls here with RedirectUrls object  
			var redirUrls = new RedirectUrls()
			{
				cancel_url = redirectUrl + "&Cancel=true",
				return_url = redirectUrl
			};
			// Adding Tax, shipping and Subtotal details  
			var details = new Details()
			{
				tax = "1",
				shipping = "1",
				subtotal = "1"
			};
			//Final amount with details  
			var amount = new Amount()
			{
				currency = "USD",
				total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
				details = details
			};
			var transactionList = new List<Transaction>();
			// Adding description about the transaction  
			var paypalOrderId = DateTime.Now.Ticks;
			transactionList.Add(new Transaction()
			{
				description = $"Invoice #{paypalOrderId}",
				invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
				amount = amount,
				item_list = itemList
			});
			this.payment = new Payment()
			{
				intent = "sale",
				payer = payer,
				transactions = transactionList,
				redirect_urls = redirUrls
			};
			// Create a payment using a APIContext  
			return this.payment.Create(apiContext);
		}


	}
}
