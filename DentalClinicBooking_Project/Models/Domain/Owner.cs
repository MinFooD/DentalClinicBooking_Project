using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Owner
{
    public Guid OwnerId { get; set; }

    public string OwnerName { get; set; } = null!;

    public string Experience { get; set; } = null!;

    public string Image { get; set; } = null!;

    public Guid? AccountId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();
}
