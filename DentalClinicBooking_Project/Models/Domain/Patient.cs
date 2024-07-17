﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;
public partial class Patient
{
    public Guid PatientId { get; set; }

    public string PatientName { get; set; }

    public string Phone { get; set; }

    public DateOnly BirthDay { get; set; }

    public bool Gender { get; set; }

    public string Address { get; set; }

    public string CitizenIdentificationCard { get; set; }

    public string Nation { get; set; }

    public string Job { get; set; }

    public string HealthInsuranceCardCode { get; set; }

    public Guid? AccountId { get; set; }

    public virtual Account Account { get; set; }

    public virtual ICollection<ClinicAppointmentSchedule> ClinicAppointmentSchedules { get; set; } = new List<ClinicAppointmentSchedule>();
}