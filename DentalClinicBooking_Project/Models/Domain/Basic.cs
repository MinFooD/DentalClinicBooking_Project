﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.Domain;

public partial class Basic
{
    public Guid BasicId { get; set; }
    [Required()]
    public string BasicName { get; set; }
    [Required()]
    public string Address { get; set; }
    [Required()]
    public string Phone { get; set; }

    public Guid? ClinicId { get; set; }

    public virtual Clinic Clinic { get; set; }

    public virtual ICollection<ClinicAppointmentSchedule> ClinicAppointmentSchedules { get; set; } = new List<ClinicAppointmentSchedule>();

    public virtual ICollection<Dentist> Dentists { get; set; } = new List<Dentist>();
}