using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using DentalClinicBooking_Project.Models.ViewModels.ViewScheduleModels;
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
        private readonly ISlotRepository slotRepository;

        public ViewAppointmentScheduleController(
            IHttpContextAccessor httpContextAccessor,
            IClinicAppointmentScheduleRepository clinicAppointmentScheduleRepository,
            IClinicRepository clinicRepository,
            ISlotRepository slotRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.clinicAppointmentScheduleRepository = clinicAppointmentScheduleRepository;
            this.clinicRepository = clinicRepository;
            this.slotRepository = slotRepository;
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAppointmentSchedule(string? searchQuery)
        {
            string? patientString = httpContextAccessor.HttpContext?.Session.GetString("patient");
            if (!string.IsNullOrEmpty(patientString))
            {
                var patient = JsonConvert.DeserializeObject<Patient>(patientString);
                IEnumerable<ClinicAppointmentSchedule> list = new List<ClinicAppointmentSchedule>();

                if (string.IsNullOrWhiteSpace(searchQuery) == true)
                {
                    list = await clinicAppointmentScheduleRepository.GetAllAsync(patient?.PatientId ?? Guid.Empty);
                }
                else
                {
                    list = await clinicAppointmentScheduleRepository.SearchAsync(searchQuery, patient?.PatientId ?? Guid.Empty);
                }
                
                    var model = list.Select(a => new DisplaySchedule
                    {
                        Id = a.ClinicAppointmentScheduleId,
                        ClinicName = a.Clinic.ClinicName,
                        BasicAddress = a.Basic.Address,
                        Code = a.Code,
                        Date = a.Date,
                        BasicName = a.Basic.BasicName,
                        MainImage = a.Clinic.MainImage,
                        Service = a.Service.ServiceName,
                        PatientName = a.Patient.PatientName,
                        PatientAddress = a.Patient.Address,
                        Gender = DisplaySchedule.GetGender(a.Patient.Gender),
                        BirthDate = a.Patient.BirthDay,
                        SlotOfClinics = slotRepository.Get(a.ClinicId ?? Guid.Empty, a.SlotId ?? Guid.Empty),
                    }).ToList();
                              
                return View(model);
            }

            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> CancelAppointment(Guid id)
        {
            var model = await clinicAppointmentScheduleRepository.DeleteAsyn(id);
            return RedirectToAction("DisplayAppointmentSchedule");
        }
    }
}
