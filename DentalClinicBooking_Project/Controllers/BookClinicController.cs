using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Reflection;
using static System.Reflection.Metadata.BlobBuilder;

namespace DentalClinicBooking_Project.Controllers
{
    public class BookClinicController : Controller
    {
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

            var arrSlots = await slotRepository.GetAllSlotsAsync();
            var slots = new Dictionary<Guid, int>();

            for (int i = 0; i < arrSlots.Length; i++)
            {
                slots.Add(arrSlots[i].SlotId, 0);
            }

            foreach (var booking in bookings)
            {
                if (slots.ContainsKey(booking?.SlotId ?? Guid.Empty))
                {
                    slots[booking?.SlotId ?? Guid.Empty] = booking?.Count ?? 0;
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
                    Type = "Book Appointment"
                };

                var model = await clinicAppointmentScheduleRepository.GetDuplicateAsync(clinicAppointmentSchedule);

                if (model == null)
                {
                    model = await clinicAppointmentScheduleRepository.AddAsync(clinicAppointmentSchedule);
                    return Ok(new
                    {
                        redirectUrl = Url.Action("ConfirmBooking", new { id = model.ClinicAppointmentScheduleId }),
                        success = true
                    });
                }
                else
                {
                    return Ok(new { success = false});
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
                    PatientAddress = patient?.Address
                };
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

    }
}
