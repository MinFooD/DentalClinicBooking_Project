using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels
{
    public class ClinicAppointmentViewModel
    {
        public string? PatientName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? PatientAddress { get; set; }
        public IEnumerable<ClinicAppointmentSchedule> ClinicAppointmentSchedules { get; set; } = new List<ClinicAppointmentSchedule>();

    }
}
