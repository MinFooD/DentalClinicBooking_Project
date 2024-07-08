﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using DentalClinicBooking_Project.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicBooking_Project.Data;

public partial class DentalClinicBookingProjectContext : DbContext
{
    public DentalClinicBookingProjectContext()
    {
    }
    public DentalClinicBookingProjectContext(DbContextOptions<DentalClinicBookingProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Basic> Basics { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<ClinicAppointmentSchedule> ClinicAppointmentSchedules { get; set; }

    public virtual DbSet<ClinicImage> ClinicImages { get; set; }

    public virtual DbSet<Dentist> Dentists { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<SlotOfClinic> SlotOfClinics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Server=PHUC\\PHUC;uid=sa;pwd=123456;database= DentalCLinicBookingProject;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA586406D0D8F");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AccountID");
            entity.Property(e => e.Gmail)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E81527203E");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.AccountId, "UQ__Admin__349DA5873844C9A4").IsUnique();

            entity.Property(e => e.AdminId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AdminID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");

            entity.HasOne(d => d.Account).WithOne(p => p.Admin)
                .HasForeignKey<Admin>(d => d.AccountId)
                .HasConstraintName("FK__Admin__AccountID__6EF57B66");
        });

        modelBuilder.Entity<Basic>(entity =>
        {
            entity.HasKey(e => e.BasicId).HasName("PK__Basic__BABA01D14E038419");

            entity.ToTable("Basic");

            entity.Property(e => e.BasicId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("BasicID");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.BasicName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ClinicId).HasColumnName("CLinicID");
            entity.Property(e => e.LinkAddress).IsUnicode(false);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.Clinic).WithMany(p => p.Basics)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FK__Basic__CLinicID__6FE99F9F");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__Clinic__3347C2FD7550AC67");

            entity.ToTable("Clinic");

            entity.Property(e => e.ClinicId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ClinicID");
            entity.Property(e => e.ClinicName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("CLinicName");
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.MainImage)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

            entity.HasOne(d => d.Owner).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Clinic__OwnerID__70DDC3D8");
        });

        modelBuilder.Entity<ClinicAppointmentSchedule>(entity =>
        {
            entity.HasKey(e => e.ClinicAppointmentScheduleId).HasName("PK__CLinicAp__E5FDDBE0AC2969AA");

            entity.ToTable("CLinicAppointmentSchedule");

            entity.Property(e => e.ClinicAppointmentScheduleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CLinicAppointmentScheduleID");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.BasicName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ClinicName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Service).HasMaxLength(100);
            entity.Property(e => e.SlotName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.ClinicAppointmentSchedules)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__CLinicApp__Patie__71D1E811");
        });

        modelBuilder.Entity<ClinicImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ClinicIm__7516F4EC0E0CB16E");

            entity.ToTable("ClinicImage");

            entity.Property(e => e.ImageId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ImageID");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.Image)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Clinic).WithMany(p => p.ClinicImages)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FK__ClinicIma__Clini__72C60C4A");
        });

        modelBuilder.Entity<Dentist>(entity =>
        {
            entity.HasKey(e => e.DentistId).HasName("PK__Dentist__9157336F23029816");

            entity.ToTable("Dentist");

            entity.HasIndex(e => e.AccountId, "UQ__Dentist__349DA587C00604DC").IsUnique();

            entity.Property(e => e.DentistId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("DentistID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.BasicId).HasColumnName("BasicID");
            entity.Property(e => e.DentistName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Experience)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithOne(p => p.Dentist)
                .HasForeignKey<Dentist>(d => d.AccountId)
                .HasConstraintName("FK__Dentist__Account__73BA3083");

            entity.HasOne(d => d.Basic).WithMany(p => p.Dentists)
                .HasForeignKey(d => d.BasicId)
                .HasConstraintName("FK__Dentist__BasicID__74AE54BC");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__Owner__81938598A07E9BE6");

            entity.ToTable("Owner");

            entity.HasIndex(e => e.AccountId, "UQ__Owner__349DA587C07B46FF").IsUnique();

            entity.Property(e => e.OwnerId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("OwnerID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Experience).IsRequired();
            entity.Property(e => e.Image)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OwnerName)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.Owner)
                .HasForeignKey<Owner>(d => d.AccountId)
                .HasConstraintName("FK__Owner__AccountID__75A278F5");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC34671E4D6F7");

            entity.ToTable("Patient");

            entity.HasIndex(e => e.AccountId, "UQ__Patient__349DA587A2EE4988").IsUnique();

            entity.Property(e => e.PatientId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PatientID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.CitizenIdentificationCard)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.HealthInsuranceCardCode)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Job).HasMaxLength(50);
            entity.Property(e => e.Nation).HasMaxLength(20);
            entity.Property(e => e.PatientName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.AccountId)
                .HasConstraintName("FK__Patient__Account__76969D2E");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EA86EE4A88");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(d => d.Clinics).WithMany(p => p.Services)
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceOfClinic",
                    r => r.HasOne<Clinic>().WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ServiceOf__Clini__778AC167"),
                    l => l.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ServiceOf__Servi__787EE5A0"),
                    j =>
                    {
                        j.HasKey("ServiceId", "ClinicId").HasName("PK__ServiceO__062FCCC5A609B270");
                        j.ToTable("ServiceOfClinic");
                        j.IndexerProperty<Guid>("ServiceId").HasColumnName("ServiceID");
                        j.IndexerProperty<Guid>("ClinicId").HasColumnName("ClinicID");
                    });
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__Slot__0A124A4F133859CE");

            entity.ToTable("Slot");

            entity.Property(e => e.SlotId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("SlotID");
            entity.Property(e => e.SlotName)
                .IsRequired()
                .HasMaxLength(10);
        });

        modelBuilder.Entity<SlotOfClinic>(entity =>
        {
            entity.HasKey(e => new { e.SlotId, e.ClinicId }).HasName("PK__SlotOfCl__C926366016B60044");

            entity.ToTable("SlotOfClinic");

            entity.Property(e => e.SlotId).HasColumnName("SlotID");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

            entity.HasOne(d => d.Clinic).WithMany(p => p.SlotOfClinics)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SlotOfCli__Clini__797309D9");

            entity.HasOne(d => d.Slot).WithMany(p => p.SlotOfClinics)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SlotOfCli__SlotI__7A672E12");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}