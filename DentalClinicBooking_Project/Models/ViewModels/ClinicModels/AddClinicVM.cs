using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels.ClinicModels
{
    public class AddClinicVM
    {
        public string ClinicName { get; set; }

        public string MainImage { get; set; }

        public string Description { get; set; }

        public Guid OwnerId { get; set; }

        public List<string> ImageUrls { get; set; } = new List<string>();
        public string ImageUrlsJson { get; set; }

        public List<Guid> ServiceIds { get; set; } = new List<Guid>();

        public List<ServiceVM> AvailableServices { get; set; } = new List<ServiceVM>();

        public List<SlotVM> Slots { get; set; } = new List<SlotVM>();
    }
}
