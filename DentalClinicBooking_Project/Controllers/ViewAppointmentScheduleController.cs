using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DentalClinicBooking_Project.Controllers
{
    public class ViewAppointmentScheduleController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IClinicAppointmentScheduleRepository clinicAppointmentScheduleRepository;
        private readonly IClinicRepository clinicRepository;

        public ViewAppointmentScheduleController(
            IHttpContextAccessor httpContextAccessor,
            IClinicAppointmentScheduleRepository clinicAppointmentScheduleRepository,
            IClinicRepository clinicRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.clinicAppointmentScheduleRepository = clinicAppointmentScheduleRepository;
            this.clinicRepository = clinicRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> DisplayAppointmentSchedule()
        {
            string? patientString = httpContextAccessor.HttpContext?.Session.GetString("patient");
            if (!string.IsNullOrEmpty(patientString))
            {
                var patient = JsonConvert.DeserializeObject<Patient>(patientString);
                var list = await clinicAppointmentScheduleRepository.GetAllAsync(patient?.PatientId ?? Guid.Empty);
                var model = list.Select(async a => new AppointmentBookingSuccess
                {
                   Code = a.Code,

                }).ToList();
                return View(list);
            }

            return RedirectToAction("Login", "Login");
        }
    }
}
