﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicBooking_Project.Models.Domain;

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

    public virtual DbSet<DescriptionClinic> DescriptionClinics { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<SlotOfClinic> SlotOfClinics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=PHUC;uid=sa;pwd=12345;database= DentalCLinicBookingProject;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5869109BF4E");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AccountID");
            entity.Property(e => e.Gmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E83866CB04");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.AccountId, "UQ__Admin__349DA58736711019").IsUnique();

            entity.Property(e => e.AdminId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AdminID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");

            entity.HasOne(d => d.Account).WithOne(p => p.Admin)
                .HasForeignKey<Admin>(d => d.AccountId)
                .HasConstraintName("FK__Admin__AccountID__60A75C0F");
        });

        modelBuilder.Entity<Basic>(entity =>
        {
            entity.HasKey(e => e.BasicId).HasName("PK__Basic__BABA01D130591E16");

            entity.ToTable("Basic");

            entity.Property(e => e.BasicId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("BasicID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BasicName).HasMaxLength(100);
            entity.Property(e => e.ClinicId).HasColumnName("CLinicID");
            entity.Property(e => e.LinkAddress).IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.Clinic).WithMany(p => p.Basics)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FK__Basic__CLinicID__5165187F");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__Clinic__3347C2FDC0DAD24D");

            entity.ToTable("Clinic");

            entity.Property(e => e.ClinicId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ClinicID");
            entity.Property(e => e.ClinicName)
                .HasMaxLength(100)
                .HasColumnName("CLinicName");
            entity.Property(e => e.MainImage)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

            entity.HasOne(d => d.Owner).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Clinic__OwnerID__45F365D3");
        });

        modelBuilder.Entity<ClinicAppointmentSchedule>(entity =>
        {
            entity.HasKey(e => e.ClinicAppointmentScheduleId).HasName("PK__CLinicAp__E5FDDBE0BC81A79D");

            entity.ToTable("CLinicAppointmentSchedule");

            entity.Property(e => e.ClinicAppointmentScheduleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CLinicAppointmentScheduleID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BasicName).HasMaxLength(100);
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.SlotName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.ClinicAppointmentSchedules)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__CLinicApp__Patie__6477ECF3");
        });

        modelBuilder.Entity<ClinicImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ClinicIm__7516F4EC16550B3E");

            entity.ToTable("ClinicImage");

            entity.Property(e => e.ImageId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ImageID");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Clinic).WithMany(p => p.ClinicImages)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FK__ClinicIma__Clini__4D94879B");
        });

        modelBuilder.Entity<Dentist>(entity =>
        {
            entity.HasKey(e => e.DentistId).HasName("PK__Dentist__9157336F4F197606");

            entity.ToTable("Dentist");

            entity.HasIndex(e => e.AccountId, "UQ__Dentist__349DA58765725D8D").IsUnique();

            entity.Property(e => e.DentistId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("DentistID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.BasicId).HasColumnName("BasicID");
            entity.Property(e => e.DentistName).HasMaxLength(100);
            entity.Property(e => e.Experience)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithOne(p => p.Dentist)
                .HasForeignKey<Dentist>(d => d.AccountId)
                .HasConstraintName("FK__Dentist__Account__571DF1D5");

            entity.HasOne(d => d.Basic).WithMany(p => p.Dentists)
                .HasForeignKey(d => d.BasicId)
                .HasConstraintName("FK__Dentist__BasicID__5629CD9C");
        });

        modelBuilder.Entity<DescriptionClinic>(entity =>
        {
            entity.HasKey(e => e.DescriptionId).HasName("PK__Descript__A58A9FEBDACCDE4E");

            entity.ToTable("DescriptionClinic");

            entity.Property(e => e.DescriptionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("DescriptionID");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.Clinic).WithMany(p => p.DescriptionClinics)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FK__Descripti__Clini__49C3F6B7");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__Owner__81938598ACA18CEE");

            entity.ToTable("Owner");

            entity.HasIndex(e => e.AccountId, "UQ__Owner__349DA58756A5D445").IsUnique();

            entity.Property(e => e.OwnerId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("OwnerID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OwnerName).HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.Owner)
                .HasForeignKey<Owner>(d => d.AccountId)
                .HasConstraintName("FK__Owner__AccountID__4222D4EF");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC346C7FD92C2");

            entity.ToTable("Patient");

            entity.HasIndex(e => e.AccountId, "UQ__Patient__349DA587DFF47A84").IsUnique();

            entity.Property(e => e.PatientId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PatientID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CitizenIdentificationCard)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.HealthInsuranceCardCode)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Job).HasMaxLength(50);
            entity.Property(e => e.Nation).HasMaxLength(20);
            entity.Property(e => e.PatientName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.AccountId)
                .HasConstraintName("FK__Patient__Account__5BE2A6F2");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EA4986A3B2");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(100);

            entity.HasMany(d => d.Clinics).WithMany(p => p.Services)
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceOfClinic",
                    r => r.HasOne<Clinic>().WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ServiceOf__Clini__68487DD7"),
                    l => l.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ServiceOf__Servi__6754599E"),
                    j =>
                    {
                        j.HasKey("ServiceId", "ClinicId").HasName("PK__ServiceO__062FCCC5CB1BEB47");
                        j.ToTable("ServiceOfClinic");
                        j.IndexerProperty<Guid>("ServiceId").HasColumnName("ServiceID");
                        j.IndexerProperty<Guid>("ClinicId").HasColumnName("ClinicID");
                    });
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__Slot__0A124A4FA6A7D8A7");

            entity.ToTable("Slot");

            entity.Property(e => e.SlotId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("SlotID");
            entity.Property(e => e.SlotName).HasMaxLength(10);
        });

        modelBuilder.Entity<SlotOfClinic>(entity =>
        {
            entity.HasKey(e => new { e.SlotId, e.ClinicId }).HasName("PK__SlotOfCl__C9263660D812688F");

            entity.ToTable("SlotOfClinic");

            entity.Property(e => e.SlotId).HasColumnName("SlotID");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

            entity.HasOne(d => d.Clinic).WithMany(p => p.SlotOfClinics)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SlotOfCli__Clini__6C190EBB");

            entity.HasOne(d => d.Slot).WithMany(p => p.SlotOfClinics)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SlotOfCli__SlotI__6B24EA82");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
