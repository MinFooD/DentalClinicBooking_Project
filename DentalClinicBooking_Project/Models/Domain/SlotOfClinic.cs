using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class SlotOfClinic
{
    public Guid SlotId { get; set; }

    public Guid ClinicId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual Clinic Clinic { get; set; } = null!;

    public virtual Slot Slot { get; set; } = null!;
}
