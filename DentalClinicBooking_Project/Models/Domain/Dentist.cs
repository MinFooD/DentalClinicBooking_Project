﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Dentist
{
    public Guid DentistId { get; set; }

    public string DentistName { get; set; }

    public string Image { get; set; }

    public string Experience { get; set; }

    public string Description { get; set; }

    public Guid? BasicId { get; set; }

    public Guid? AccountId { get; set; }

    public virtual Account Account { get; set; }

    public virtual Basic Basic { get; set; }
}