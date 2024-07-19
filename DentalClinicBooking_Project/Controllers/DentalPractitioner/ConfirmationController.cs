using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.Dentist.Confirmation;
using DentalClinicBooking_Project.Models.ViewModels.ViewScheduleModels;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Controllers.DentalPractitioner
{
    public class ConfirmationController : Controller
    {
        private readonly IClinicAppointmentScheduleRepository clinicAppointmentScheduleRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISlotRepository slotRepository;

        public ConfirmationController(
            IClinicAppointmentScheduleRepository clinicAppointmentScheduleRepository,
            IHttpContextAccessor httpContextAccessor,
            ISlotRepository slotRepository)
        {
            this.clinicAppointmentScheduleRepository = clinicAppointmentScheduleRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.slotRepository = slotRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ShowAppointmentConfirmation(string code)
        {
            var appointmentSchedule = await clinicAppointmentScheduleRepository.GetAsync(code);
            var model = new DisplayBookingInformation();
            if (string.IsNullOrEmpty(code))
            {
                return View(model);
            }

            string? dentistString = httpContextAccessor.HttpContext?.Session.GetString("dentist");
            if (!string.IsNullOrEmpty(dentistString))
            {
                var dentist = JsonConvert.DeserializeObject<Dentist>(dentistString);                  
                if (appointmentSchedule?.BasicId == dentist?.BasicId)
                {
                    DateOnly dateModel = appointmentSchedule?.Date ?? DateOnly.MinValue;
                    DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
                    if (dateModel < currentDate)
                    {
                        ViewBag.Message = "Phiếu khám đã hết hạn!";
                    }
                    else
                    {
                        model = new DisplayBookingInformation
                        {
                            Id = appointmentSchedule.ClinicAppointmentScheduleId,
                            ClinicName = appointmentSchedule.Clinic.ClinicName,
                            BasicAddress = appointmentSchedule.Basic.Address,
                            Code = appointmentSchedule.Code,
                            Date = appointmentSchedule.Date,
                            BasicName = appointmentSchedule.Basic.BasicName,
                            MainImage = appointmentSchedule.Clinic.MainImage,
                            Service = appointmentSchedule.Service.ServiceName,
                            PatientName = appointmentSchedule.Patient.PatientName,
                            PatientAddress = appointmentSchedule.Patient.Address,
                            Gender = DisplaySchedule.GetGender(appointmentSchedule.Patient.Gender),
                            BirthDate = appointmentSchedule.Patient.BirthDay,
                            SlotOfClinics = slotRepository.Get(appointmentSchedule.ClinicId ?? Guid.Empty, appointmentSchedule.SlotId ?? Guid.Empty),
                        };
                    }                   
                }
                else
                {
                    ViewBag.Message = "Mã của phiếu khám không hợp lệ!";
                }
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> ShowAppointmentConfirmation(Guid id)
        {

            return RedirectToAction("Confirmation", "ShowAppointmentConfirmation");
        }
    }
}
