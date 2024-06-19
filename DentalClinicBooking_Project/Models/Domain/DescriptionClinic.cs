using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class DescriptionClinic
{
    public Guid DescriptionId { get; set; }

    public string Content { get; set; } = null!;

    public string Type { get; set; } = null!;

    public Guid? ClinicId { get; set; }

    public virtual Clinic? Clinic { get; set; }
}
