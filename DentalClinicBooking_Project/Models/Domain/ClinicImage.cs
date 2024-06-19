using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class ClinicImage
{
    public Guid ImageId { get; set; }

    public string Image { get; set; } = null!;

    public Guid? ClinicId { get; set; }

    public virtual Clinic? Clinic { get; set; }
}
