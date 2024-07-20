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
                    else if (appointmentSchedule?.Status == true)
                    {
                        ViewBag.Message = "Phiếu khám đã xác nhận";
                    }
                    else
                    {
                        model = new DisplayBookingInformation
                        {
                            Id = appointmentSchedule?.ClinicAppointmentScheduleId ?? Guid.Empty,
                            ClinicName = appointmentSchedule?.Clinic.ClinicName,
                            BasicAddress = appointmentSchedule?.Basic.Address,
                            Code = appointmentSchedule?.Code,
                            Date = appointmentSchedule?.Date ?? DateOnly.FromDateTime(DateTime.Now),
                            BasicName = appointmentSchedule?.Basic.BasicName,
                            MainImage = appointmentSchedule?.Clinic.MainImage,
                            Service = appointmentSchedule?.Service.ServiceName,
                            PatientName = appointmentSchedule?.Patient.PatientName,
                            PatientAddress = appointmentSchedule?.Patient.Address,
                            Gender = DisplayBookingInformation.GetGender(appointmentSchedule?.Patient.Gender),
                            BirthDate = appointmentSchedule?.Patient.BirthDay,
                            SlotOfClinics = slotRepository.Get(appointmentSchedule?.ClinicId ?? Guid.Empty, appointmentSchedule?.SlotId ?? Guid.Empty),
                            Status = DisplayBookingInformation.GetStatus(appointmentSchedule?.Status)
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
            var AppointmentSchedule = await clinicAppointmentScheduleRepository.GetAsync(id);
            if (AppointmentSchedule != null)
            {
                AppointmentSchedule.Status = true;
            }           
            var model = await clinicAppointmentScheduleRepository.UpdateStatusAsync(AppointmentSchedule ?? new ClinicAppointmentSchedule());

            return RedirectToAction("ShowAppointmentConfirmation", "Confirmation", new { code = AppointmentSchedule?.Code });
        }
    }
}
