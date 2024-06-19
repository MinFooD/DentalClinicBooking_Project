using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Clinic
{
    public Guid ClinicId { get; set; }

    public string ClinicName { get; set; } = null!;

    public string MainImage { get; set; } = null!;

    public Guid? OwnerId { get; set; }

    public virtual ICollection<Basic> Basics { get; set; } = new List<Basic>();

    public virtual ICollection<ClinicImage> ClinicImages { get; set; } = new List<ClinicImage>();

    public virtual ICollection<DescriptionClinic> DescriptionClinics { get; set; } = new List<DescriptionClinic>();

    public virtual Owner? Owner { get; set; }

    public virtual ICollection<SlotOfClinic> SlotOfClinics { get; set; } = new List<SlotOfClinic>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
