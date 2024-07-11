
using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using DentalClinicBooking_Project.Models.ViewModels.Dentist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            return RedirectToAction("HomePage","Home");
        }
        [HttpGet]
        public IActionResult UpdateDentist(Guid id)
        {
            string ownerString = _contx.HttpContext.Session.GetString("owner");
            if (!string.IsNullOrEmpty(ownerString))
            {
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
                        NewPassword = string.Empty,
                        ConfirmPassword = string.Empty,
                    };
                    return View(updateDentistVM);
                }
            }
            return RedirectToAction("HomePage","Home");
        }

        [HttpPost]
        public IActionResult UpdateDentist(UpdateDentistVM updateDentistVM)
        {
            var dentist = _context.Dentists.Include(x => x.Account).FirstOrDefault(x => x.DentistId.Equals(updateDentistVM.DentistId));
            if (dentist != null)
            {
				// Kiểm tra nếu email đã tồn tại trong cơ sở dữ liệu
				bool emailExists = _context.Accounts.Any(a => a.Gmail.Equals(updateDentistVM.Gmail) && a.AccountId != dentist.Account.AccountId);

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

                if(!string.IsNullOrEmpty(updateDentistVM.NewPassword) && updateDentistVM.NewPassword.Equals(updateDentistVM.ConfirmPassword))
                {
                    dentist.Account.Password = BCrypt.Net.BCrypt.HashPassword(updateDentistVM.NewPassword);
                }
                

                _context.SaveChanges();
                TempData["result"] = "Update Successfully.";
                
            }
            else
            {
                TempData["result"] = "Update Failed.";
            }
            return RedirectToAction("ShowDentistForOwner");
        }

        public IActionResult ChooseClinicForAddDentist(string searchString, int page = 1)
        {
            _context = new DentalClinicBookingProjectContext();

            var clinics = _context.Clinics.Select(a => new ClinicWithAddress
            {
                ClinicId = a.ClinicId,
                ClinicName = a.ClinicName,
                MainImage = a.MainImage,
                Address = a.Basics.FirstOrDefault().Address
            }).Where(d => d.ClinicName.Contains(searchString) || string.IsNullOrEmpty(searchString)).ToList();

            int NoOfClinicPerPage = 5;
            var NoOfPaging = Math.Ceiling((decimal)clinics.Count() / NoOfClinicPerPage);
            int NoOfDentistToSkip = (page - 1) * NoOfClinicPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPaging = NoOfPaging;
            clinics = clinics.Skip(NoOfDentistToSkip).Take(NoOfClinicPerPage).ToList();

            return View(clinics);
        }

        public IActionResult ChooseBasicForAddDentist(Guid id)
        {
            var basic = _context.Clinics.Where(x => x.ClinicId.Equals(id)).Select(x => new BasicWithClinicName
            {
                clinicName = x.ClinicName,
                basics = x.Basics.ToList(),
            }).FirstOrDefault();
            if (basic != null)
            {
                return View(basic);
            }
            return RedirectToAction("ChooseClinicForAddDentist");
        }
        [HttpGet]
        public IActionResult AddDentist(Guid basicId)
        {
            var addDentistVM = new AddDentistVM
            {
                BasicId = basicId,
            };
            return View(addDentistVM);
        }
        [HttpPost]
        public IActionResult AddDentist(AddDentistVM addDentistVM)
        {
            if (ModelState.IsValid)
            {
                var checkGmail = _context.Accounts.FirstOrDefault(x => x.Gmail.Equals(addDentistVM.UserName));
                if(checkGmail != null)
                {
					ModelState.AddModelError("General", "Username already exists.");
					return View(addDentistVM);
				};
                var account = new Account
                {
                    Gmail = addDentistVM.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(addDentistVM.Password),
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
                };
                _context.Add(dentist);
                _context.SaveChanges();
				ModelState.AddModelError("Success", "Register successful.");
				return RedirectToAction("ShowAllDentist", "Dentist");

			}
			ModelState.AddModelError("Success", "Register successful.");
			return RedirectToAction("HomePage", "Home");
		}

    }
}
