using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Text;

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
			byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
			byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV
			if (ModelState.IsValid)
            {
				var account = _context.Accounts.FirstOrDefault(x => x.UserName.Equals(_login.UserName));
                //if(account != null && account.Password.Equals(HashPasswordController.EncryptString(_login.Password, key, iv)))
                if (account != null && _login.Password.Equals(HashPasswordController.DecryptString(account.Password,key,iv)))
                {
                    //HttpContext.Session.SetObject("account", Newtonsoft.Json.JsonConvert.SerializeObject(account));
                    //session.SetString("Gmail", account.Gmail);
                    string accountString = JsonConvert.SerializeObject(account);
                    _contx.HttpContext.Session.SetString("account", accountString);
                    if (account.RoleId == 2)
                    {
                        var patient = _context.Patients.FirstOrDefault(x => x.AccountId.Equals(account.AccountId));
                        if (patient != null)
                        {
                            string patientString = JsonConvert.SerializeObject(patient, settings);
                            _contx.HttpContext.Session.SetString("patient", patientString);
                        }
                    }
                    if (account.RoleId == 3)
                    {
                        var owner = _context.Owners.FirstOrDefault(x => x.AccountId.Equals(account.AccountId));
                        if (owner != null)
                        {
                            //cấu hình JsonSerializerSettings để bỏ qua các vòng lặp tham chiếu:
                            string patientString = JsonConvert.SerializeObject(owner, settings);
                            _contx.HttpContext.Session.SetString("owner", patientString);
                        }
                    }
					if (account.RoleId == 4)
					{
						var dentist = _context.Dentists.FirstOrDefault(x => x.AccountId.Equals(account.AccountId));
						if (dentist != null)
						{
							//cấu hình JsonSerializerSettings để bỏ qua các vòng lặp tham chiếu:
							string dentistString = JsonConvert.SerializeObject(dentist, settings);
							_contx.HttpContext.Session.SetString("dentist", dentistString);
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
            _contx.HttpContext.Session.Remove("owner");
			_contx.HttpContext.Session.Remove("dentist");

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
            byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
            byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV
            if (ModelState.IsValid)
            {
                var checkGmail = _context.Accounts.FirstOrDefault(x => x.Gmail.Equals(registerVM.Gmail) || x.UserName.Equals(registerVM.UserName));
                if (checkGmail!=null)
                {
                    ModelState.AddModelError("General", "Username or gmail already exists.");
                    return View(registerVM);
                }
                var account = new Account
                {
                    UserName = registerVM.UserName,
                    Gmail = registerVM.Gmail,
                    Password = HashPasswordController.EncryptString(registerVM.Password, key, iv),
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
