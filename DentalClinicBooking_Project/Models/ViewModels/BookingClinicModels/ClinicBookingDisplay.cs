using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels
{
    public class ClinicBookingDisplay
    {
        //public Guid ClinicId { get; set; }
        public string ClinicName { get; set; } = null!;
        public string MainImage { get; set; } = null!;
        public IEnumerable<Basic> Basics { get; set; } = new List<Basic>();
        public IEnumerable<SlotOfClinic> SlotOfClinics { get; set; } = new List<SlotOfClinic>();
        public IEnumerable<Service> Services { get; set; } = new List<Service>();
    }
}
