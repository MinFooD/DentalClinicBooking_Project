using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels.Basics
{
    public class BasicWithClinicImage
    {
        public List<Basic> basics { get; set; }

        public Clinic clinic { get; set; }
    }
}
