using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels
{
    public class BookingClinicModel
    {
        public Guid ClinicId { get; set; }
        public string ClinicName { get; set; } = null!;
        public DateOnly Day { get; set; }
        public string BasicName { get; set; } = null!;
        public virtual Clinic? Slot { get; set; }
    }
}
