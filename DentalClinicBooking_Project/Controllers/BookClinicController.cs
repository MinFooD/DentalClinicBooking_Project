using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace DentalClinicBooking_Project.Controllers
{
    public class BookClinicController : Controller
    {
        private readonly IClinicRepository bookClinicRepository;

        public BookClinicController(IClinicRepository bookClinicRepository)
        {
            this.bookClinicRepository = bookClinicRepository;
        }


        [HttpGet]
        public async Task<IActionResult> AllClinics(
            string? searchQuery,
            int pageSize = 2,
            int pageNumber = 1)
        {
            var totalRecords = await bookClinicRepository.CountAsync();
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;

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
            //var model = new ClinicBookingDisplay
            //{
            //    //ClinicId = clinic.ClinicId,
            //    ClinicName = clinic?.ClinicName,
            //    MainImage = clinic.MainImage,
            //    Basics = clinic.Basics,
            //    SlotOfClinics = clinic.SlotOfClinics,
            //    Services = clinic.Services,
            //};


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckSlots([FromBody] BookingInfo bookingModel)
        {
            bookingModel ??= new BookingInfo();

            var bookings = await bookClinicRepository
                .GetBookingsByDateAndClinicAsync(
                bookingModel.Day,
                bookingModel.ClinicName,
                bookingModel.BasicName)
                ?? new List<BookingSlot>();

            var arrSlots = await bookClinicRepository.GetAllSlotsAsync();
            var slots = new Dictionary<string, int>();

            for (int i = 0; i < arrSlots.Length; i++)
            {
                slots.Add(arrSlots[i].StartTime
                    + " - " +
                    arrSlots[i].EndTime, 0);
            }

            foreach (var booking in bookings)
            {
                if (slots.ContainsKey(booking.SlotName))
                {
                    slots[booking.SlotName] = booking.Count;
                }
            }

            return Ok(slots);
        }

        [HttpPost]
        public async Task<IActionResult> AppointmentBookingInfoAsync([FromBody] AppointmentBookingViewModel appointmentBookingModel)
        {
            ClinicAppointmentSchedule model = new ClinicAppointmentSchedule
            {
                Code = ClinicAppointmentSchedule.BookingCode(),
                PatientId = Guid.Parse("78987C55-FA52-48A1-9F2A-44803E560A40"),
                ClinicName = appointmentBookingModel.ClinicName,
                BasicName = appointmentBookingModel?.BasicName,
                Address = appointmentBookingModel?.Address,
                Date = appointmentBookingModel?.Date ?? DateOnly.FromDateTime(DateTime.Now),
                SlotName = appointmentBookingModel?.SlotName,
                Service = appointmentBookingModel?.Service,
            };

            var test = await bookClinicRepository.AddAsync(model);

            return View();
        }

    }
}
