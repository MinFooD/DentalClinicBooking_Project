using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.PatientViewModel;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DentalClinicBooking_Project.Controllers
{
    public class PatientController : Controller
    {
        DentalClinicBookingProjectContext _context;
        private IConfiguration _configuration;
        private readonly IPatientRepository patientRepository;

        public PatientController(DentalClinicBookingProjectContext context, IConfiguration configuration, IPatientRepository patientRepository)
        {
            _context = context;
            _configuration = configuration;
            this.patientRepository = patientRepository;
        }

        [HttpGet]
        public IActionResult ChangeInformation(Guid id)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.PatientId.Equals(id));
            return View(patient);
        }

        [HttpPost]
        public IActionResult ChangeInformation(Patient patientUpdate)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.PatientId.Equals(patientUpdate.PatientId));
            if (ModelState.IsValid)
            {
                if (patient == null)
                {
                    return View();
                }
                patient.PatientName = patientUpdate.PatientName;
                patient.BirthDay = patientUpdate.BirthDay;
                patient.Address = patientUpdate.Address;
                patient.Phone = patientUpdate.Phone;
                patient.HealthInsuranceCardCode = patientUpdate.HealthInsuranceCardCode;
                patient.CitizenIdentificationCard = patientUpdate.CitizenIdentificationCard;
                patient.Job = patientUpdate.Job;
                patient.Gender = patientUpdate.Gender;
                patient.Nation = patientUpdate.Nation;

                _context.SaveChanges();
            }
            return RedirectToAction("ShowProfile", new { id = patient.AccountId });
        }

        [HttpGet]
        public IActionResult ShowProfile(Guid id)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.AccountId.Equals(id));
            var patient = _context.Patients.FirstOrDefault(x => x.AccountId.Equals(id));
            var changePasswordVM = new ChangePatientPassword
            {
                AccountId = account.AccountId,
                PatientId = patient.PatientId,
                PatientName = patient.PatientName,
                NewPassword = string.Empty,
                OldPassword = string.Empty,
                Phone = patient.Phone,
                BirthDay = patient.BirthDay,
            };
            return View(changePasswordVM);
        }

        [HttpPost]
        public IActionResult ShowProfile(ChangePatientPassword cPP)
        {
			byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
			byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV
			var account = _context.Accounts.FirstOrDefault(x => x.AccountId.Equals(cPP.AccountId));
            if (account.Password.Equals(HashPasswordController.EncryptString(cPP.OldPassword, key, iv)))
			{
                account.Password = HashPasswordController.EncryptString(cPP.NewPassword, key, iv);
                _context.SaveChanges();
				TempData["result"] = "ChangePassword Successfully.";
                cPP.OldPassword = string.Empty;
                cPP.NewPassword = string.Empty;
				return View(cPP);

                //xóa session + add lại session mới
            }
            else
            {
                TempData["result"] = "ChangePassword Failed.";
                return View(cPP);
            }
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ResetPassword reset)
        {
            if (ModelState.IsValid)
            {
                byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
                byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV
                var checkAccount = _context.Accounts.FirstOrDefault(x => x.UserName.Equals(reset.UserName) && x.Gmail.Equals(reset.Gmail));
                if (checkAccount != null)
                {
                    string password = HashPasswordController.DecryptString(checkAccount.Password,key,iv);
                    string gmailSend = _configuration["EmailSettings:GmailSend"];
                    string gmailPassword = _configuration["EmailSettings:GmailPassword"];
                    string fromEmail = gmailSend;  // Gửi từ email cấu hình trong appsettings.json
                    string toEmail = checkAccount.Gmail;  // Địa chỉ email người nhận
                    string subject = "Reset Password";
                    string body = patientRepository.SendMailForm(checkAccount.UserName, password);
                    bool result =  patientRepository.SendMailGoogleSmtp(fromEmail, toEmail, subject, body, gmailSend, gmailPassword);
                    if (result)
                    {
						TempData["Success"] = "Mật khẩu đã được gửi về email của bạn.";
						return RedirectToAction("Login","Login");
                    }
                    else
                    {
                        TempData["Fail"] = "Tên đăng nhập hoặc gmail không chính xác!";
                    }
                }
                TempData["Fail"] = "Tên đăng nhập hoặc gmail không chính xác!";
            }
            return View(reset);

        }

    }
}
