using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels.Dentist
{
    public class BasicWithClinicName
    {
        public string clinicName { get; set; }

        public List<Basic> basics { get; set; }
    }
}
