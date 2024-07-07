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

    public string SlotName { get; set; }

    public string BasicName { get; set; }

    public string Code { get; set; }

    public string Address { get; set; }

    public string Service { get; set; }

    public string ClinicName { get; set; }

    public bool Status { get; set; }

    public virtual Patient Patient { get; set; }

}