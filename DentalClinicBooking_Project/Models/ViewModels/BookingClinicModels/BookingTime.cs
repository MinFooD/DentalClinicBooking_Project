namespace DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels
{
    public class BookingTime
    {
        public Guid SlotId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
