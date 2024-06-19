using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models;
using DentalClinicBooking_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DentalClinicBooking_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		public DentalClinicBookingProjectContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
		}

		public IActionResult HomePage()
		{
			_context = new DentalClinicBookingProjectContext();


			var clinics = _context.Clinics.Select(a => new ClinicWithAddress
			{
				ClinicId = a.ClinicId,
				ClinicName = a.ClinicName,
				MainImage = a.MainImage,
				Address = a.Basics.FirstOrDefault().Address
            }).ToList();

			var dentists = _context.Dentists.Select(a => new DentistWithClinicName
			{
				DentistId = a.DentistId,
				DentistName = a.DentistName,
				Image = a.Image,
				Experience = a.Experience,
				BasicId = a.BasicId,
				AccountId = a.AccountId,
				ClinicName = a.Basic.Clinic.ClinicName
			}).Take(6).ToList();

			var viewModels = new ClinicAndDentist
			{
				clinicWithAddress = clinics,
				dentistWithClinicName = dentists,
			};
			return View(viewModels);
		}


	}
}
