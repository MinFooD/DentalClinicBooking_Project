﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Basic
{
    public Guid BasicId { get; set; }

    public string BasicName { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string LinkAddress { get; set; }

    public Guid? ClinicId { get; set; }

    public virtual Clinic Clinic { get; set; }

    public virtual ICollection<Dentist> Dentists { get; set; } = new List<Dentist>();
}