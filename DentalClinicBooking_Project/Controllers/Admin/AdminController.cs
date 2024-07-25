using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.ViewModels.AdminViewModels;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DentalClinicBooking_Project.Controllers.Admin
{
	public class AdminController : Controller
	{
		byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
		byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV
		private readonly IClinicRepository clinicRepository;
		private readonly DentalClinicBookingProjectContext _context;

		public AdminController(IClinicRepository clinicRepository, DentalClinicBookingProjectContext context)
		{
			this.clinicRepository = clinicRepository;
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Home()
		{
			var list = await clinicRepository.GetAllForAdminAsync();

			return View(list);
		}

		[HttpPost]
		public IActionResult Home(Guid id)
		{
			//var clinic = await clinicRepository.GetAsync(id);
			var model = clinicRepository.UpdateStatus(id);

			return RedirectToAction("Home", "Admin");
		}

		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			//var clinic = await clinicRepository.GetAsync(id);
			var model = clinicRepository.Delete(id);

			return RedirectToAction("Home", "Admin");
		}

		public IActionResult ShowAccountForAdmin()
		{
			var account = _context.Accounts.Where(x=> x.RoleId!=1).ToList();
			return View(account);
		}
		[HttpGet]
		public IActionResult UpdateAccountByAdmin(Guid id)
		{

			var account = _context.Accounts.FirstOrDefault(x => x.AccountId.Equals(id));
			var updateAccountVM = new UpdateAccountVM
			{
				Gmail = account.Gmail,
				UserName = account.UserName,
				AccountId = account.AccountId,
				RoleId = account.RoleId,
				Password = HashPasswordController.DecryptString(account.Password, key, iv)
			};
			return View(updateAccountVM);
		}
		[HttpPost]
		public IActionResult UpdateAccountByAdmin(UpdateAccountVM updateAccountVM)
		{
			if (ModelState.IsValid)
			{
				var check = _context.Accounts.FirstOrDefault(x => (x.AccountId != updateAccountVM.AccountId) && (x.Gmail.Equals(updateAccountVM.Gmail) || x.UserName.Equals(updateAccountVM.UserName)));
				if (check != null)
				{
					return View(updateAccountVM);
				}
				var account = _context.Accounts.FirstOrDefault(x => x.AccountId.Equals(updateAccountVM.AccountId));
				account.UserName = updateAccountVM.UserName;
				account.Password = HashPasswordController.EncryptString(updateAccountVM.Password, key, iv);
				account.Gmail = updateAccountVM.Gmail;
				_context.SaveChanges();
				return RedirectToAction("ShowAccountForAdmin");
			}
			return View(updateAccountVM);
		}

		public IActionResult DeleteAccount(Guid id)
		{
			var account = _context.Accounts.FirstOrDefault(x => x.AccountId.Equals(id));
			if (account!=null && account.RoleId == 4)
			{
				_context.Accounts.Remove(account);
				var dentist = _context.Dentists.FirstOrDefault(x => x.AccountId.Equals(account.AccountId));
				_context.Dentists.Remove(dentist);
				//_context.SaveChanges();
			}
			if(account!=null && account.RoleId == 2)
			{
				var patient = _context.Patients.FirstOrDefault(x => x.AccountId.Equals(account.AccountId));
				var checkBooking = _context.ClinicAppointmentSchedules.Where(x => x.PatientId.Equals(patient.PatientId) && x.Status==false).Count();
				if (checkBooking == 0)
				{
                    _context.Accounts.Remove(account);
					_context.Patients.Remove(patient);
                    //_context.SaveChanges();
                }	
			}
			if(account!=null && account.RoleId == 3)
			{
				var owner = _context.Owners.FirstOrDefault(x => x.AccountId.Equals(account.AccountId));
				var checkBooking = _context.Owners.Where(x => x.OwnerId.Equals(owner.OwnerId)).Include(x => x.Clinics).ThenInclude(x => x.ClinicAppointmentSchedules).SelectMany(x => x.Clinics).SelectMany(x => x.ClinicAppointmentSchedules).Where(x => x.Status == false).Count();
				if(checkBooking ==0)
				{
					_context.Accounts.Remove(account);
					_context.Owners.Remove(owner);
					foreach (var clinic in owner.Clinics)
					{
						_context.Clinics.Remove(clinic);
						foreach (var basic in clinic.Basics)
						{
							_context.Basics.Remove(basic);
							foreach (var dentist in basic.Dentists)
							{
								_context.Dentists.Remove(dentist);
								_context.Accounts.Remove(dentist.Account);
							}
						}
					}
					//_context.SaveChanges();
				}
			}
			return RedirectToAction("ShowAccountForAdmin");
		}


	}
}
