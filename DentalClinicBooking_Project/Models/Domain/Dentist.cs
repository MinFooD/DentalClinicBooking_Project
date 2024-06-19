using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Dentist
{
    public Guid DentistId { get; set; }

    public string DentistName { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string Experience { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid? BasicId { get; set; }

    public Guid? AccountId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Basic? Basic { get; set; }
}
