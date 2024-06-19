using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Patient
{
    public Guid PatientId { get; set; }

    public string PatientName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly BirthDay { get; set; }

    public bool? Gender { get; set; }

    public string Address { get; set; } = null!;

    public string? CitizenIdentificationCard { get; set; }

    public string? Nation { get; set; }

    public string? Job { get; set; }

    public string? HealthInsuranceCardCode { get; set; }

    public Guid? AccountId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<ClinicAppointmentSchedule> ClinicAppointmentSchedules { get; set; } = new List<ClinicAppointmentSchedule>();
}
