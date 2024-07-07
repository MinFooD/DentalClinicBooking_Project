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
