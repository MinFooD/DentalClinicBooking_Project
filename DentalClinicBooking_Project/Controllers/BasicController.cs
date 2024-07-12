using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.Basics;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinicBooking_Project.Controllers
{
    public class BasicController : Controller
    {
        DentalClinicBookingProjectContext _context;

        public BasicController(DentalClinicBookingProjectContext context)
        {
            _context = context;
        }

        public IActionResult ShowAllBasicForOwner(Guid id)
        {
            var basics = _context.Basics.Where(x => x.ClinicId.Equals(id)).ToList();
            if (basics == null)
            {
                return RedirectToAction("ShowAllClinicForOwner", "Clinic");
            }
            var clinic = _context.Clinics.FirstOrDefault(x => x.ClinicId.Equals(id));
            var basicVm = new BasicWithClinicImage
            {
                clinic = clinic,
                basics = basics
            };
            return View(basicVm);
        }

        [HttpGet]
        public IActionResult AddBasic(Guid id)
        {
            Basic basic = new Basic
            {
                ClinicId = id
            };
            return View(basic);
        }

        [HttpPost]
        public IActionResult AddBasic(Basic basic)
        {
            if (ModelState.IsValid)
            {
                _context.Basics.Add(basic);
                _context.SaveChanges();
                return RedirectToAction("ShowAllClinicForOwner", "Clinic");
            }
            return View(basic);

        }
    }
}
