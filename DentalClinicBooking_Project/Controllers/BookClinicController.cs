using DentalClinicBooking_Project.Models.ViewModels;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> ShowAllClinics(
            string? searchQuery,
            int pageSize = 2,
            int pageNumber = 1)
        {
            //var totalRecords = await bookClinicRepository.CountAsync();
            //var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            //if (pageNumber > totalPages)
            //{
            //    pageNumber--;
            //}

            //if (pageNumber < 1)
            //{
            //    pageNumber++;
            //}

            ViewBag.SearchQuery = searchQuery;
            //ViewBag.TotalPages = totalPages;            
            //ViewBag.PageSize = pageSize;
            //ViewBag.PageNumber = pageNumber;

            var Model = await bookClinicRepository.GetAllAsync(searchQuery, pageNumber, pageSize);

            return View(Model);
        }


        [HttpGet]
        public async Task<IActionResult> ShowClinicDetails(Guid id)
        {
            var Clinic = await bookClinicRepository.GetAsync(id);
            var descriptionClinics = Clinic?.DescriptionClinics;

            if (Clinic != null && descriptionClinics != null)
            {
                var Model = new ClinicDetailsModel
                {
                    Id = Clinic.ClinicId,
                    ClinicName = Clinic.ClinicName,
                    MainImage = Clinic.MainImage,
                    DescriptionClinics = descriptionClinics
                };

                return View(Model);
            }

            return RedirectToAction("ShowAllClinics");
        }

        //[HttpGet]
        //public async Task<IActionResult> BookingClinic()
        //{
        //    var Slot = await bookClinicRepository.GetAsync(1);

        //    return View(Slot);
        //}

        //[HttpPost]
        //public IActionResult BookingClinic([FromBody] BookingClinic bookingModel)
        //{
        //    ClinicAppointmentSchedule clinicAppointmentSchedule = new ClinicAppointmentSchedule();
        //    return View();
        //}

    }
}
