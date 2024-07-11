namespace DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels
{
    public class AppointmentBookingViewModel
    {
        public Guid ClinicId { get; set; }
        public Guid BasicId { get; set; }
        public DateOnly Date { get; set; }
        public Guid SlotId { get; set; }
        public  Guid ServiceId { get; set; }
        public static string BookingCode()
        {
            const string prefix = "YMA";
            string datePart = DateTime.Now.ToString("yyMMdd");
            Random random = new Random();
            string randomPart = random.Next(1000, 10000).ToString();
            return prefix + datePart + randomPart;
        }
    }
}
