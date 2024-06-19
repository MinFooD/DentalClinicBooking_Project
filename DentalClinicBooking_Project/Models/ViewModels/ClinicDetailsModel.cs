using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels
{
    public class ClinicDetailsModel
    {
        public Guid Id { get; set; }
        public string ClinicName { get; set; } = null!;

        public string MainImage { get; set; } = null!;

        public virtual ICollection<DescriptionClinic> DescriptionClinics { get; set; } = new List<DescriptionClinic>();
    }
}
