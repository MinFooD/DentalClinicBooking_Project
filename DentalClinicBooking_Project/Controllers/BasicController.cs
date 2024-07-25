using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BasicViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DentalClinicBooking_Project.Controllers
{
	public class BasicController : Controller
	{
		DentalClinicBookingProjectContext _context;
		private readonly IHttpContextAccessor _contx;

		public BasicController(DentalClinicBookingProjectContext context, IHttpContextAccessor contx)
		{
			_context = context;
			_contx = contx;
		}

		public IActionResult ShowAllBasicForOwner()
		{
			string ownerString = _contx.HttpContext.Session.GetString("owner");
			if (!string.IsNullOrEmpty(ownerString))
			{
				var owner = JsonConvert.DeserializeObject<Owner>(ownerString);
				var basics = _context.Owners.Include(x => x.Clinics).ThenInclude(x => x.Basics).Where(x => x.OwnerId.Equals(owner.OwnerId)).SelectMany(x => x.Clinics).SelectMany(z => z.Basics.Select(y => new BasicWithClinicImage
				{
					Basic = y,
					MainImage = z.MainImage,
				})).ToList();
				if (basics == null)
				{
					return RedirectToAction("ShowAllClinicForOwner", "Clinic");
				}
				return View(basics);
			}
			return View();
		}

		[HttpGet]
		public IActionResult AddBasic()
		{
			string ownerString = _contx.HttpContext.Session.GetString("owner");
			if (!string.IsNullOrEmpty(ownerString))
			{
				var owner = JsonConvert.DeserializeObject<Owner>(ownerString);
				var basicVM = new BasicVM
				{
					clicics = _context.Owners.Include(x => x.Clinics).Where(x => x.OwnerId.Equals(owner.OwnerId)).SelectMany(x => x.Clinics).Where(x=> x.Status==true).ToList(),
				};
				return View(basicVM);
			}
			return View();
		}

		[HttpPost]
		public IActionResult AddBasic(BasicVM basicVM)
		{
			if (ModelState.IsValid)
			{
				bool checkAddressExits = _context.Basics.Any(a => a.Address.Equals(basicVM.Address));
				if (checkAddressExits)
				{
					return View(basicVM);
				}
				var basic = new Basic
				{
					BasicName = basicVM.BasicName,
					Phone = basicVM.Phone,
					Address = basicVM.Address,
					ClinicId = basicVM.ClinicId,
				};
				_context.Basics.Add(basic);
				_context.SaveChanges();
				return RedirectToAction("ShowAllBasicForOwner", "Basic");
			}
			return View(basicVM);

		}

		[HttpGet]
		public IActionResult UpdateBasic(Guid id)
		{
			string ownerString = _contx.HttpContext.Session.GetString("owner");
			if (!string.IsNullOrEmpty(ownerString))
			{
				var owner = JsonConvert.DeserializeObject<Owner>(ownerString);
				var basic = _context.Basics.FirstOrDefault(x => x.BasicId.Equals(id));
				if (basic != null)
				{
					var basicVM = new BasicVM
					{
						BasicName = basic.BasicName,
						Phone = basic.Phone,
						Address = basic.Address,
						BasicId = basic.BasicId,
						ClinicId = basic.ClinicId,
						clicics = _context.Owners.Include(x => x.Clinics).Where(x => x.OwnerId.Equals(owner.OwnerId)).SelectMany(x => x.Clinics).ToList(),
					};
					return View(basicVM);
				}
			}
			return View();
		}

		[HttpPost]
		public IActionResult UpdateBasic(BasicVM basicVM)
		{
			if (ModelState.IsValid)
			{
				bool checkAddressExits = _context.Basics.Any(a => a.Address.Equals(basicVM.Address) && a.BasicId != basicVM.BasicId);
				if (checkAddressExits)
				{
					TempData["result"] = "Update Failed. Address already exists.";
					return View(basicVM);
				}
				var basicc = _context.Basics.FirstOrDefault(x => x.BasicId.Equals(basicVM.BasicId));
				if (basicc != null)
				{
					basicc.Address = basicVM.Address;
					basicc.BasicName = basicVM.BasicName;
					basicc.Phone = basicVM.Phone;
					basicc.ClinicId = basicVM.ClinicId;

					_context.SaveChanges();
					TempData["result"] = "Update Successfully.";
					return RedirectToAction("ShowAllBasicForOwner", "Basic");
				}
			}
			else
			{
				TempData["result"] = "Update Failed.";
			}
			return View(basicVM);
		}

		public IActionResult DeleteBasic(Guid id)
		{
			var checkBookingSchedule = _context.ClinicAppointmentSchedules.Where(x=> x.BasicId.Equals(id)).Count();
			if (checkBookingSchedule == 0)
			{
				var basic = _context.Basics.Include(x => x.Dentists).ThenInclude(x => x.Account).Include(x => x.ClinicAppointmentSchedules).FirstOrDefault(x => x.BasicId.Equals(id));
				//var basic1 = _context.Basics.FirstOrDefault(x => x.BasicId.Equals(id)).Include(x => x.Dentists).ThenInclude(x => x.Account).Include(x => x.ClinicAppointmentSchedules);
				foreach (var dentist in basic.Dentists)
				{
					_context.Accounts.Remove(dentist.Account);
					_context.Dentists.Remove(dentist);
					_context.SaveChanges();
				}
				if (basic == null)
				{
					return RedirectToAction("ShowAllBasicForOwner", "Basic");
				}
				_context.Basics.Remove(basic);
				_context.SaveChanges();
			}
            return RedirectToAction("ShowAllBasicForOwner", "Basic");
        }
	}
}
