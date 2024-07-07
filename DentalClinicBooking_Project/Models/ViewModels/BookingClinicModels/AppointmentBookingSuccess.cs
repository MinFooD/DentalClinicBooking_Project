namespace DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels
{
    public class AppointmentBookingSuccess
    {
        public string? ClinicName { get; set; }      
        public string? ClinicAddress { get; set; }
        public string? Code { get; set; }
        public DateOnly Date { get; set; }
        public string? SlotName { get; set; }
        public string? BasicName { get; set; }               
        public string? Service { get; set; }
        public string? PatientName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? PatientAddress { get; set; }
    }
}
