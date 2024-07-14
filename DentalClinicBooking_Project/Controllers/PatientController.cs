using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult ShowProfile(Guid id)
        {
            var patient = _context.Patients.Include(x => x.Account).FirstOrDefault(x => x.PatientId.Equals(id));
            return View(patient);
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
            if (ModelState.IsValid)
            {
                var patient = _context.Patients.FirstOrDefault(x => x.PatientId.Equals(patientUpdate.PatientId));
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
            return RedirectToAction("ChangeInformation", new { id = patientUpdate.PatientId });
        }
    }
}
