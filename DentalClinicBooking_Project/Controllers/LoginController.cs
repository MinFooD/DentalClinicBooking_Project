using DentalClinicBooking_Project.Data;
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
                            string patientString = JsonConvert.SerializeObject(patient);
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
    }
}
