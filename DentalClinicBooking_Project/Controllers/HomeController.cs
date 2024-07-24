using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DentalClinicBooking_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		public DentalClinicBookingProjectContext _context;
        private readonly IHttpContextAccessor _contx;

        public HomeController(ILogger<HomeController> logger, DentalClinicBookingProjectContext context, IHttpContextAccessor contx)
        {
            _logger = logger;
            _context = context;
            _contx = contx;
        }

        public IActionResult HomePage()
		{
			var clinics = _context.Clinics.Where(x=> x.Status == true).Select(a => new ClinicWithAddress
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

		public IActionResult Dashboard()
		{
			int[] data1 = new int[12];

			for (int i = 1; i <= 12; i++)
			{
				int count = _context.ClinicAppointmentSchedules.Where(x => x.Date != null && x.Date.Month == i).Count();
				data1[i - 1] = count;
			}

			ViewBag.dataArrayForLineChart = data1;

			int[] data2 = new int[3];
			data2[0] = data1[0] + data1[1] + data1[2] + data1[3];
			data2[1] = data1[4] + data1[5] + data1[6] + data1[7];
			data2[2] = data1[8] + data1[9] + data1[10] + data1[11];

			ViewBag.dataArrayForPieChart = data2;

			int[] data3 = new int[4];
			data3[0] = data1[0] + data1[1] + data1[2];
			data3[1] = data1[3] + data1[4] + data1[5];
			data3[2] = data1[6] + data1[7] + data1[8];
			data3[3] = data1[9] + data1[10] + data1[11];

			ViewBag.dataArrayForBarChart = data3;
			return View();
		}

		public IActionResult HomePageOwner()
		{
            string ownerString = _contx.HttpContext.Session.GetString("owner");
			if (!string.IsNullOrEmpty(ownerString))
			{
				var owner = JsonConvert.DeserializeObject<Owner>(ownerString);
				int countDentist = _context.Owners.Where(x=> x.OwnerId.Equals(owner.OwnerId)).SelectMany(x => x.Clinics).SelectMany(x => x.Basics).SelectMany(x=> x.Dentists).Count();
				int countClinic = _context.Owners.Where(x => x.OwnerId.Equals(owner.OwnerId)).SelectMany(x => x.Clinics).Count();
				int countBasic = _context.Owners.Where(x => x.OwnerId.Equals(owner.OwnerId)).SelectMany(x => x.Clinics).SelectMany(x => x.Basics).Count();

				ViewBag.countDentist = countDentist;
				ViewBag.countClinic = countClinic;
				ViewBag.countBasic = countBasic;
				ViewBag.owner = owner;
                return View();
			}
			return View();
		}


	}
}
