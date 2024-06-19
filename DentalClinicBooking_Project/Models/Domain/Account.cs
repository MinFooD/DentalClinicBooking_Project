using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Account
{
    public Guid AccountId { get; set; }

    public string Gmail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual Dentist? Dentist { get; set; }

    public virtual Owner? Owner { get; set; }

    public virtual Patient? Patient { get; set; }
}
