using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Basic
{
    public Guid BasicId { get; set; }

    public string BasicName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? LinkAddress { get; set; }

    public Guid? ClinicId { get; set; }

    public virtual Clinic? Clinic { get; set; }

    public virtual ICollection<Dentist> Dentists { get; set; } = new List<Dentist>();
}
