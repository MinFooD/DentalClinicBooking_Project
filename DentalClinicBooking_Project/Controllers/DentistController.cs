﻿
using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using DentalClinicBooking_Project.Models.ViewModels.Dentist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text;

namespace DentalClinicBooking_Project.Controllers
{
    public class DentistController : Controller
    {
        public DentalClinicBookingProjectContext _context;

        private readonly IHttpContextAccessor _contx;

        public DentistController(DentalClinicBookingProjectContext context, IHttpContextAccessor contx)
        {
            _context = context;
            _contx = contx;
        }

        public IActionResult ShowDentistInfo(Guid? id)
        {
            var dentist = _context.Dentists.Select(a => new DentistWithClinicName
            {
                DentistId = a.DentistId,
                DentistName = a.DentistName,
                Image = a.Image,
                Experience = a.Experience,
                BasicId = a.BasicId,
                AccountId = a.AccountId,
                ClinicName = a.Basic.Clinic.ClinicName,
				ClinicId = a.Basic.Clinic.ClinicId,
				Description = a.Description,
                Address = a.Basic.Address
            }).FirstOrDefault(x => x.DentistId.Equals(id));

            if (dentist != null)
            {
                return View(dentist);
            }
            return View(null);
        }

        [HttpGet]
        public IActionResult ShowAllDentist(string searchString, int page = 1)
        {
            _context = new DentalClinicBookingProjectContext();

            var dentists = _context.Dentists.Select(a => new DentistWithClinicName
            {
                DentistId = a.DentistId,
                DentistName = a.DentistName,
                Image = a.Image,
                Experience = a.Experience,
                BasicId = a.BasicId,
                AccountId = a.AccountId,
                ClinicName = a.Basic.Clinic.ClinicName,
                ClinicId = a.Basic.Clinic.ClinicId,
                Address = a.Basic.Address
            }).Where(d => d.DentistName.Contains(searchString) || string.IsNullOrEmpty(searchString)).ToList();

            var searchDentists = new SearchDentist
            {
                SearchString = searchString,
                dentists = dentists,
                ResultCount = dentists.Count()//tổng số phần tử
            };

            //paging
            int NoOfDentistPerPage = 4;//bao nhiêu phần tử mỗi trang
            var NoOfPaging = Math.Ceiling((decimal)dentists.Count() / NoOfDentistPerPage);//tồng số trang
            int NoOfDentistToSkip = (page - 1) * NoOfDentistPerPage;//bỏ qua bao nhiêu
            ViewBag.Page = page;//trang bao nhiêu
            ViewBag.NoOfPaging = NoOfPaging;
            searchDentists.dentists = searchDentists.dentists.Skip(NoOfDentistToSkip).Take(NoOfDentistPerPage).ToList();
            //

            return View(searchDentists);


        }
        [HttpGet]
        public IActionResult ShowDentistForOwner()
        {
            string ownerString = _contx.HttpContext.Session.GetString("owner");
            if (!string.IsNullOrEmpty(ownerString))
            {
                var owner = JsonConvert.DeserializeObject<Owner>(ownerString);
                if (owner != null)
                {
                    var dentist = _context.Owners.Where(x => x.OwnerId.Equals(owner.OwnerId)).SelectMany(x => x.Clinics).SelectMany(x => x.Basics).SelectMany(x => x.Dentists).Include(d => d.Account).ToList();
                    return View(dentist);
                }
            }
            return RedirectToAction("HomePageOwner","Home");
        }
        [HttpGet]
        public IActionResult UpdateDentist(Guid id)
        {
			byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
			byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV
			string ownerString = _contx.HttpContext.Session.GetString("owner");
            if (!string.IsNullOrEmpty(ownerString))
            {
				var owner = JsonConvert.DeserializeObject<Owner>(ownerString);
				var dentist = _context.Dentists.Include(x => x.Account).FirstOrDefault(x => x.DentistId.Equals(id));
                if (dentist != null)
                {
                    var updateDentistVM = new UpdateDentistVM
                    {
                        DentistId = dentist.DentistId,
                        DentistName = dentist.DentistName,
                        Description = dentist.Description,
                        Image = dentist.Image,
                        Experience = dentist.Experience,
                        Gmail = dentist.Account.Gmail,
                        UserName = dentist.Account.UserName,
                        Password = HashPasswordController.DecryptString(dentist.Account.Password, key, iv),
                        Phone = dentist.Phone,
                        Gender = dentist.Gender,
                        BasicId = dentist.BasicId,
						basics = _context.Owners.Include(x => x.Clinics).ThenInclude(x => x.Basics).Where(x => x.OwnerId.Equals(owner.OwnerId)).SelectMany(x => x.Clinics).SelectMany(x => x.Basics).ToList(),
					};
                    return View(updateDentistVM);
                }
            }
            return RedirectToAction("ShowDentistForOwner", "Dentist");
        }

        [HttpPost]
        public IActionResult UpdateDentist(UpdateDentistVM updateDentistVM)
        {
			byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
			byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV
			var dentist = _context.Dentists.Include(x => x.Account).FirstOrDefault(x => x.DentistId.Equals(updateDentistVM.DentistId));
            if (dentist != null)
            {
				// Kiểm tra nếu email đã tồn tại trong cơ sở dữ liệu
				bool emailExists = _context.Accounts.Any(a => (a.Gmail.Equals(updateDentistVM.Gmail) || a.UserName.Equals(updateDentistVM.UserName)) && a.AccountId != dentist.Account.AccountId);

				if (emailExists)
				{
					TempData["result"] = "Update Failed. Email already exists.";
					return RedirectToAction("ShowDentistForOwner");
				}
				dentist.DentistName = updateDentistVM.DentistName;
                dentist.Description = updateDentistVM.Description;
                dentist.Experience = updateDentistVM.Experience;
                dentist.Image = updateDentistVM.Image;
                dentist.Account.Gmail = updateDentistVM.Gmail;
                dentist.Account.UserName = updateDentistVM.UserName;
				dentist.Account.Password = HashPasswordController.EncryptString(updateDentistVM.Password, key, iv);
                dentist.BasicId= updateDentistVM.BasicId;
                dentist.Phone = updateDentistVM.Phone;
                dentist.Gender = updateDentistVM.Gender;

				_context.SaveChanges();
                TempData["result"] = "Update Successfully.";
            }
            else
            {
                TempData["result"] = "Update Failed.";
            }
            return RedirectToAction("ShowDentistForOwner");
        }

        [HttpGet]
        public IActionResult AddDentist()
        {
			string ownerString = _contx.HttpContext.Session.GetString("owner");
            if (!string.IsNullOrEmpty(ownerString))
            {
                var owner = JsonConvert.DeserializeObject<Owner>(ownerString);
                var addDentistVM = new AddDentistVM
                {
                    basics = _context.Owners.Include(x => x.Clinics).ThenInclude(x => x.Basics).Where(x => x.OwnerId.Equals(owner.OwnerId)).SelectMany(x => x.Clinics).SelectMany(x => x.Basics).ToList(),
                };
				return View(addDentistVM);
			}
            return View();
        }

        [HttpPost]
        public IActionResult AddDentist(AddDentistVM addDentistVM)
        {
			byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
			byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV
			if (ModelState.IsValid)
            {
                var checkGmail = _context.Accounts.FirstOrDefault(x => x.Gmail.Equals(addDentistVM.UserName) || x.UserName.Equals(addDentistVM.UserName));
                if(checkGmail != null)
                {
					ModelState.AddModelError("General", "Username already exists.");
					return View(addDentistVM);
				};
                var account = new Account
                {
                    UserName = addDentistVM.UserName,
					Gmail = addDentistVM.Gmail,
                    Password = HashPasswordController.EncryptString(addDentistVM.Password, key, iv),
					RoleId = 4,
                };
                _context.Add(account);
                _context.SaveChanges();
                var dentist = new Dentist
                {
                    DentistName = addDentistVM.DentistName,
                    Description = addDentistVM.Description,
                    Image = addDentistVM.Image,
                    Experience = addDentistVM.Experience,
                    AccountId = account.AccountId,
                    BasicId = addDentistVM.BasicId,
                    Phone = addDentistVM.Phone,
                    Gender = addDentistVM.Gender,
                };
                _context.Add(dentist);
                _context.SaveChanges();
				ModelState.AddModelError("Success", "Register successful.");
				return RedirectToAction("ShowDentistForOwner", "Dentist");

			}
			ModelState.AddModelError("Success", "Register successful.");
			return View(addDentistVM);
		}

        [HttpGet]
        public IActionResult DeleteDentist(Guid id)
        {
            var dentist = _context.Dentists.FirstOrDefault(x => x.DentistId.Equals(id));
            if (dentist != null)
            {
                _context.Remove(dentist);
                _context.SaveChanges();
            }
            return RedirectToAction("ShowDentistForOwner");
        }

    }
}
