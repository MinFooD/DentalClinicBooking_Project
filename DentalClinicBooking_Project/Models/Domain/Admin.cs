﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DentalClinicBooking_Project.Models.Domain;
public partial class Admin
{
    public Guid AdminId { get; set; }

    public Guid? AccountId { get; set; }

    public virtual Account Account { get; set; }
}