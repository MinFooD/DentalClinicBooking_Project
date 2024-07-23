﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class ClinicAppointmentSchedule
{
    public Guid ClinicAppointmentScheduleId { get; set; }

    public DateOnly Date { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? SlotId { get; set; }

    public Guid? BasicId { get; set; }

    public string Code { get; set; }

    public Guid? ServiceId { get; set; }

    public Guid? ClinicId { get; set; }

    public decimal? Price { get; set; }

    public bool? Status { get; set; }

    public virtual Basic Basic { get; set; }

    public virtual Clinic Clinic { get; set; }

    public virtual Patient Patient { get; set; }

    public virtual Service Service { get; set; }

    public virtual Slot Slot { get; set; }
}