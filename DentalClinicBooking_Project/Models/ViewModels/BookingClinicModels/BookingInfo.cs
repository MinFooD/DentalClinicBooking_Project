namespace DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels
{
    public class BookingInfo
    {
        public string ClinicName { get; set; } = null!;
        public DateOnly Day { get; set; }
        public string BasicName { get; set; } = null!;
    }
}
