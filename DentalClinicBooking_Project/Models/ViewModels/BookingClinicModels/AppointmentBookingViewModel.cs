namespace DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels
{
    public class AppointmentBookingViewModel
    {
        public string ClinicName { get; set; }
        public string BasicName { get; set; }
        public string Address { get; set; }
        public DateOnly Date { get; set; }
        public string SlotName { get; set; }
        public  string Service { get; set; }
    }
}
