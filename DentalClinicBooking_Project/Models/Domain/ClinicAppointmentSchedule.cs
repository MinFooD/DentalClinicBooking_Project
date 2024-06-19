using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class ClinicAppointmentSchedule
{
    public Guid ClinicAppointmentScheduleId { get; set; }

    public DateOnly Day { get; set; }

    public Guid? PatientId { get; set; }

    public string SlotName { get; set; } = null!;

    public string BasicName { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual Patient? Patient { get; set; }
}
