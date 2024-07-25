using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.OwnerViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal;
using System.Text;

namespace DentalClinicBooking_Project.Controllers
{
    public class OwnerController : Controller
    {
		byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
		byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV
		private readonly DentalClinicBookingProjectContext _context;

		public OwnerController(DentalClinicBookingProjectContext context)
		{
			_context = context;
		}

		public IActionResult ShowAllOwnerForAdmin()
        {
            var owner = _context.Owners.ToList();
            return View(owner);
        }
        [HttpGet]
        public IActionResult AddOwner()
        {
            return View();
        }

        public IActionResult AddOwner(AddOwnerVM addOwnerVM)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Accounts.FirstOrDefault(x => x.Gmail.Equals(addOwnerVM.Gmail) || x.UserName.Equals(addOwnerVM.UserName));
                if(check != null)
                {
                    return View(addOwnerVM);
                }
                var account = new Models.Domain.Account
                {
                    Gmail = addOwnerVM.Gmail,
                    UserName = addOwnerVM.UserName,
                    Password = HashPasswordController.EncryptString(addOwnerVM.Password, key, iv)
                };
                _context.Accounts.Add(account);

                var owner = new Owner
                {
                    OwnerName = addOwnerVM.OwnerName,
                    Experience = addOwnerVM.Experience,
                    Image = addOwnerVM.Image,
                    AccountId = account.AccountId,
                };
                _context.Owners.Add(owner);

                _context.SaveChanges();
            }
            return View(addOwnerVM);
        }

        [HttpGet]
        public IActionResult UpdateOwner(Guid id)
        {
            var owner = _context.Owners.FirstOrDefault(x => x.OwnerId.Equals(id));
			var account = _context.Accounts.FirstOrDefault(x => x.AccountId.Equals(owner.AccountId));
			var updateOwnerVM = new UpdateOwnerVM
            {
                AccountId = account.AccountId,
                Gmail = account.Gmail,
                UserName = account.UserName,
                Password = HashPasswordController.DecryptString(account.Password, key, iv),
                OwnerName = owner.OwnerName,
                Experience = owner.Experience,
                Image = owner.Image,
                OwnerId = account?.AccountId,
            };
            return View(updateOwnerVM);
        }

        [HttpPost]
		public IActionResult UpdateOwner(UpdateOwnerVM updateOwnerVM)
        {
            var check = _context.Accounts.FirstOrDefault(x=> (x.Gmail.Equals(updateOwnerVM.Gmail)||x.UserName.Equals(updateOwnerVM.UserName))&&(x.AccountId!=updateOwnerVM.AccountId));
            if(check != null)
            {
                return View(updateOwnerVM);
            }
            var account = _context.Accounts.FirstOrDefault(x => x.AccountId.Equals(updateOwnerVM.AccountId));
            var owner = _context.Owners.FirstOrDefault(x => x.OwnerId.Equals(updateOwnerVM.OwnerId));

            account.UserName = updateOwnerVM.UserName;
            account.Password = HashPasswordController.EncryptString(updateOwnerVM.Password, key, iv);
            account.Gmail = updateOwnerVM.Gmail;
            owner.OwnerName = updateOwnerVM.OwnerName;
            owner.Experience = updateOwnerVM.Experience;
            owner.Image = updateOwnerVM.Image;

            _context.SaveChanges();

            return RedirectToAction("ShowAllOwnerForAdmin");
        }

        public IActionResult DeleteOwner(Guid id)
        {
			var owner = _context.Owners.FirstOrDefault(x => x.OwnerId.Equals(id));
			var account = _context.Accounts.FirstOrDefault(x => x.AccountId.Equals(owner.AccountId));
			var checkBooking = _context.Owners.Where(x => x.OwnerId.Equals(owner.OwnerId)).Include(x => x.Clinics).ThenInclude(x => x.ClinicAppointmentSchedules).SelectMany(x => x.Clinics).SelectMany(x => x.ClinicAppointmentSchedules).Where(x => x.Status == false).Count();
			if (checkBooking == 0)
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
                _context.SaveChanges();
            }
			return RedirectToAction("ShowAllOwnerForAdmin");
        }

	}
}
