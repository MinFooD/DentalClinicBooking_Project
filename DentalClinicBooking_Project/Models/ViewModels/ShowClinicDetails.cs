﻿
using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels
{
    public class ShowClinicDetails
    {
        public Guid Id { get; set; }
        public string ClinicName { get; set; } = null!;
        public string MainImage { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
        public virtual IEnumerable<ClinicImage> ClinicImages { get; set; } = new List<ClinicImage>();
        public virtual IEnumerable<SlotOfClinic> SlotOfClinics { get; set; } = new List<SlotOfClinic>();      
    }
}
