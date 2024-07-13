using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.Basics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                bool checkAddressExits = _context.Basics.Any(a => a.Address.Equals(basic.Address));
                if (checkAddressExits)
                {
					return View(basic);
				}
                _context.Basics.Add(basic);
                _context.SaveChanges();
                return RedirectToAction("ShowAllBasicForOwner", "Basic", new { id = basic.ClinicId});
            }
            return View(basic);

        }

        [HttpGet]
        public IActionResult UpdateBasic(Guid id)
        {
            var basic = _context.Basics.FirstOrDefault(x => x.BasicId.Equals(id));
            return View(basic);
        }

        [HttpPost]
        public IActionResult UpdateBasic(Basic basic)
        {
            if (ModelState.IsValid)
            {
                bool checkAddressExits = _context.Basics.Any(a => a.Address.Equals(basic.Address) && a.BasicId != basic.BasicId);
                if (checkAddressExits)
                {
                    TempData["result"] = "Update Failed. Address already exists.";
                    return View(basic);
                }
                var basicc = _context.Basics.FirstOrDefault(x => x.BasicId.Equals(basic.BasicId));
                if (basicc != null)
                {
                    basicc.Address = basic.Address;
                    basicc.BasicName = basic.BasicName;
                    basicc.Phone = basic.Phone;

                    _context.SaveChanges();
                    TempData["result"] = "Update Successfully.";
					return RedirectToAction("ShowAllBasicForOwner", "Basic", new { id = basic.ClinicId });
				}
            }
            else
            {
                TempData["result"] = "Update Failed.";
            }
				return View(basic);
        }
        
        public IActionResult DeleteBasic(Guid id)
        {
            var basic = _context.Basics.Include(x => x.Dentists).ThenInclude(x => x.Account).Include(x => x.ClinicAppointmentSchedules).FirstOrDefault(x => x.BasicId.Equals(id));

            foreach (var dentist in basic.Dentists)
            {
                _context.Accounts.Remove(dentist.Account);
                _context.Dentists.Remove(dentist);
            }

            foreach (var schedule in basic.ClinicAppointmentSchedules)
            {
                _context.ClinicAppointmentSchedules.Remove(schedule);
            }

            if (basic == null)
			{
				return RedirectToAction("ShowAllClinicForOwner", "Clinic");
			}
            _context.Basics.Remove(basic);
			return View();
        }
    }
}
