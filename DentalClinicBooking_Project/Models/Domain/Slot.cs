using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Slot
{
    public Guid SlotId { get; set; }

    public string SlotName { get; set; } = null!;

    public virtual ICollection<SlotOfClinic> SlotOfClinics { get; set; } = new List<SlotOfClinic>();
}
