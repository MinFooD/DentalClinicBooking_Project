using DCB1.Models.ViewModels;
using DentalClinicBooking_Project.Models;
using DentalClinicBooking_Project.Models.Domain;
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
			
			return View();
		}


	}
}
