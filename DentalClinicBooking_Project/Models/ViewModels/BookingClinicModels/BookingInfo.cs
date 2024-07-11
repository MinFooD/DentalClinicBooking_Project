namespace DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels
{
    public class BookingInfo
    {
        public Guid clinicId { get; set; }
        public DateOnly date { get; set; }
        public Guid basicId { get; set; } 
    }
}
