using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels
{
    public class ShowBookingClinic
    {
        public string ClinicName { get; set; } = null!;
        public string MainImage { get; set; } = null!;
        public virtual IEnumerable<Basic> Basics { get; set; } = new List<Basic>();
        public virtual IEnumerable<SlotOfClinic> SlotOfClinics { get; set; } = new List<SlotOfClinic>();
        public virtual IEnumerable<Service> Services { get; set; } = new List<Service>();
    }
}
