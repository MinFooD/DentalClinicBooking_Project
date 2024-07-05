using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;

namespace DentalClinicBooking_Project.Controllers
{
    public class LoginController : Controller
    {
        public DentalClinicBookingProjectContext _context;

        private readonly IHttpContextAccessor _contx;

        public JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };//cấu hình JsonSerializerSettings để bỏ qua các vòng lặp tham chiếu:

        public LoginController(DentalClinicBookingProjectContext context, IHttpContextAccessor contx)
        {
            _context = context;
            _contx = contx;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM _login)
        {
            //var session = HttpContext.Session;
            if(ModelState.IsValid)
            {
				var account = _context.Accounts.FirstOrDefault(x => x.Gmail.Equals(_login.Gmail));
                if(account != null && BCrypt.Net.BCrypt.Verify(_login.Password, account.Password))
                {
                    if (account.RoleId == 2)
                    {
                        //HttpContext.Session.SetObject("account", Newtonsoft.Json.JsonConvert.SerializeObject(account));
                        //session.SetString("Gmail", account.Gmail);
                        string accountString = JsonConvert.SerializeObject(account);
                        _contx.HttpContext.Session.SetString("account", accountString);

                        var patient = _context.Patients.Where(x => x.AccountId.Equals(account.AccountId));
                        if (patient != null)
                        {
                            //HttpContext.Session.SetString("patient", Newtonsoft.Json.JsonConvert.SerializeObject(patient));
                            //session.SetString("PatientName",patient.PatientName);
                            //var settings = new JsonSerializerSettings
                            //{
                            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            //};//cấu hình JsonSerializerSettings để bỏ qua các vòng lặp tham chiếu:
                            string patientString = JsonConvert.SerializeObject(patient,settings);
                            _contx.HttpContext.Session.SetString("patient", patientString);
                        }
                    }

                    
                    return RedirectToAction("HomePage", "Home");
                }
                
			}
            else
            {
				ModelState.AddModelError("", "Invalid login attempt.");
			}
            
            return View(_login);
        }


        [HttpGet]
        public IActionResult Logout()
        {
            _contx.HttpContext.Session.Remove("account");
            _contx.HttpContext.Session.Remove("patient");

            return RedirectToAction("Login", "Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM registerVM)
        {
            if(ModelState.IsValid)
            {
                var checkGmail = _context.Accounts.FirstOrDefault(x => x.Gmail.Equals(registerVM.UserName));
                if (checkGmail!=null)
                {
                    ModelState.AddModelError("General", "Username already exists.");
                    return View(registerVM);
                }
                var account = new Account
                {
                    Gmail = registerVM.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password),
                    RoleId = 2,
                };
                _context.Accounts.Add(account);
                _context.SaveChanges();

                var patient = new Patient
                {
                    PatientName = registerVM.PatientName,
                    Phone = registerVM.Phone,
                    BirthDay = registerVM.BirthDay,
                    Gender = registerVM.Gender,
                    Address = registerVM.Address,
                    CitizenIdentificationCard = registerVM.CitizenIdentificationCard,
                    Nation = registerVM.Nation,
                    Job = registerVM.Job,
                    HealthInsuranceCardCode = registerVM.HealthInsuranceCardCode,
                    AccountId = account.AccountId
                };
                _context.Patients.Add(patient);
                _context.SaveChanges();


                // Tự động đăng nhập sau khi đăng ký thành công
                string accountString = JsonConvert.SerializeObject(account,settings);
                _contx.HttpContext.Session.SetString("account", accountString);

                
                string patientString = JsonConvert.SerializeObject(patient, settings);
                _contx.HttpContext.Session.SetString("patient", patientString);

                ModelState.AddModelError("Success", "Register successful.");
                return RedirectToAction("HomePage", "Home");
            }
            ModelState.AddModelError("Fail", "Register Fail");
            return View(registerVM);
        }
    }
}
