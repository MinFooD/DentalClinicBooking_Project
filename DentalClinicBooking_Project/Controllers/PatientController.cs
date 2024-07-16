using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using DentalClinicBooking_Project.Models.ViewModels.PatientViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DentalClinicBooking_Project.Controllers
{
    public class PatientController : Controller
    {
        DentalClinicBookingProjectContext _context;

        public PatientController(DentalClinicBookingProjectContext context)
        {
            _context = context;
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
            }
            else
            {
                TempData["result"] = "ChangePassword Failed.";
                return View(cPP);
            }
        }
    }
}
