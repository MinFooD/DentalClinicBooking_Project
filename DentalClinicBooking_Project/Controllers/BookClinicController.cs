using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using static System.Reflection.Metadata.BlobBuilder;

namespace DentalClinicBooking_Project.Controllers
{
    public class BookClinicController : Controller
    {
        private readonly IClinicRepository bookClinicRepository;
        private readonly IClinicAppointmentScheduleRepository clinicAppointmentScheduleRepository;
        private readonly ISlotRepository slotRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IHttpContextAccessor _contx;

        public BookClinicController(IClinicRepository bookClinicRepository,
            IClinicAppointmentScheduleRepository clinicAppointmentScheduleRepository,
            ISlotRepository slotRepository,
            IPatientRepository patientRepository,
            IHttpContextAccessor contx)
        {
            this.bookClinicRepository = bookClinicRepository;
            this.clinicAppointmentScheduleRepository = clinicAppointmentScheduleRepository;
            this.slotRepository = slotRepository;
            this.patientRepository = patientRepository;
            _contx = contx;
        }


        [HttpGet]
        public async Task<IActionResult> AllClinics(
            string? searchQuery,
            int pageSize = 2,
            int pageNumber = 1)
        {
            var totalRecords = await bookClinicRepository.CountAsync(searchQuery);
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);//bao nhiêu trang

            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.PageSize = pageSize;//số phần tử mỗi trang
            ViewBag.PageNumber = pageNumber;//trang bao nhiêu

            var Model = await bookClinicRepository.GetAllAsync(searchQuery, pageNumber, pageSize);

            return View(Model);
        }


        [HttpGet]
        public async Task<IActionResult> ClinicDetails(Guid id)
        {
            var clinic = await bookClinicRepository.GetAsync(id);

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
            var clinic = await bookClinicRepository.GetAsync(id);
            var model = new ClinicBookingDisplay
            {
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
                .GetBookingsByDateAndClinicAsync(
                bookingModel.Day,
                bookingModel.ClinicName,
                bookingModel.BasicName)
                ?? new List<BookingSlot>();

            var arrSlots = await slotRepository.GetAllSlotsAsync();
            var slots = new Dictionary<string, int>();

            for (int i = 0; i < arrSlots.Length; i++)
            {
                slots.Add(
                    arrSlots[i].StartTime.ToString("HH:mm")
                    + " - " +
                    arrSlots[i].EndTime.ToString("HH:mm"), 0);
            }

            foreach (var booking in bookings)
            {
                if (slots.ContainsKey(booking.SlotName!))
                {
                    slots[booking.SlotName!] = booking.Count;
                }
            }

            return Ok(slots);
        }

        [HttpPost]
        public async Task<IActionResult> AppointmentBookingInfo([FromBody] AppointmentBookingViewModel appointmentBookingModel)
        {
            string patientString = _contx.HttpContext.Session.GetString("patient");
            if (!string.IsNullOrEmpty(patientString))
            {
                var patient = JsonConvert.DeserializeObject<Patient>(patientString);
                ClinicAppointmentSchedule clinicAppointmentSchedule = new ClinicAppointmentSchedule
                {
                    Code = AppointmentBookingViewModel.BookingCode(),
                    PatientId = patient!.PatientId,
                    ClinicName = appointmentBookingModel.ClinicName,
                    BasicName = appointmentBookingModel?.BasicName,
                    Address = appointmentBookingModel?.Address,
                    Date = appointmentBookingModel?.Date ?? DateOnly.FromDateTime(DateTime.Now),
                    SlotName = appointmentBookingModel?.SlotName,
                    Service = appointmentBookingModel?.Service,
                };

                //Còn trường hợp người dùng không thể chọn cùng 1 slot trong một ngày

                var model = await clinicAppointmentScheduleRepository.AddAsync(clinicAppointmentSchedule);

                return Ok(new { redirectUrl = Url.Action("ConfirmBooking", new { id = model.ClinicAppointmentScheduleId }) });
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmBooking(Guid id)
        {

            string patientString = _contx.HttpContext.Session.GetString("patient");
            if (!string.IsNullOrEmpty(patientString))
            {
                var patient = JsonConvert.DeserializeObject<Patient>(patientString);
                var clinicAppointmentSchedule = await clinicAppointmentScheduleRepository.GetAsync(id);

                // Thiếu thông tin bệnh nhân
                var model = new AppointmentBookingSuccess
                {
                    ClinicName = clinicAppointmentSchedule?.ClinicName!,
                    ClinicAddress = clinicAppointmentSchedule?.Address!,
                    Code = clinicAppointmentSchedule?.Code!,
                    Date = clinicAppointmentSchedule?.Date ?? DateOnly.FromDateTime(DateTime.Now)!,
                    SlotName = clinicAppointmentSchedule?.SlotName!,
                    BasicName = clinicAppointmentSchedule?.BasicName!,
                    Service = clinicAppointmentSchedule?.Service!,
                    PatientName = patient?.PatientName,
                    BirthDate = patient?.BirthDay,
                    Gender = patient?.Gender,
                    PatientAddress = patient?.Address
                };

                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> ViewSchedule()
        {
            //var patient = await patientRepository.GetAsync(Guid.Parse("78987C55-FA52-48A1-9F2A-44803E560A40"));
            //var model = new ClinicAppointmentViewModel
            //{
            //    PatientName = patient?.PatientName,
            //    BirthDate = patient?.BirthDay,
            //    Gender = patient?.Gender,
            //    PatientAddress = patient?.Address,
            //    ClinicAppointmentSchedules = patient!.ClinicAppointmentSchedules!,
            //};

            return View();
        }

    }
}
