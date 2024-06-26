
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
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA586B3C4F5AB");

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
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E837D3FB8A");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.AccountId, "UQ__Admin__349DA58778050BBF").IsUnique();

            entity.Property(e => e.AdminId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AdminID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");

            entity.HasOne(d => d.Account).WithOne(p => p.Admin)
                .HasForeignKey<Admin>(d => d.AccountId)
                .HasConstraintName("FK__Admin__AccountID__71D1E811");
        });

        modelBuilder.Entity<Basic>(entity =>
        {
            entity.HasKey(e => e.BasicId).HasName("PK__Basic__BABA01D1D70A8419");

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
                .HasConstraintName("FK__Basic__CLinicID__72C60C4A");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__Clinic__3347C2FD3E15686E");

            entity.ToTable("Clinic");

            entity.Property(e => e.ClinicId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ClinicID");
            entity.Property(e => e.ClinicName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("CLinicName");
            entity.Property(e => e.MainImage)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

            entity.HasOne(d => d.Owner).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Clinic__OwnerID__73BA3083");
        });

        modelBuilder.Entity<ClinicAppointmentSchedule>(entity =>
        {
            entity.HasKey(e => e.ClinicAppointmentScheduleId).HasName("PK__CLinicAp__E5FDDBE0BC81A79D");

            entity.ToTable("ClinicAppointmentSchedule");

            entity.Property(e => e.ClinicAppointmentScheduleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CLinicAppointmentScheduleID");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);
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
            entity.Property(e => e.SlotName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.ClinicAppointmentSchedules)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CLinicApp__Patie__6477ECF3");
        });

        modelBuilder.Entity<ClinicImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ClinicIm__7516F4EC4469F862");

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
                .HasConstraintName("FK__ClinicIma__Clini__75A278F5");
        });

        modelBuilder.Entity<Dentist>(entity =>
        {
            entity.HasKey(e => e.DentistId).HasName("PK__Dentist__9157336F47DB1F9C");

            entity.ToTable("Dentist");

            entity.HasIndex(e => e.AccountId, "UQ__Dentist__349DA58744D67006").IsUnique();

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
                .HasConstraintName("FK__Dentist__Account__76969D2E");

            entity.HasOne(d => d.Basic).WithMany(p => p.Dentists)
                .HasForeignKey(d => d.BasicId)
                .HasConstraintName("FK__Dentist__BasicID__778AC167");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__Owner__81938598ECBA16E4");

            entity.ToTable("Owner");

            entity.HasIndex(e => e.AccountId, "UQ__Owner__349DA5870BF3EFCA").IsUnique();

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
                .HasConstraintName("FK__Owner__AccountID__797309D9");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC3467A06428E");

            entity.ToTable("Patient");

            entity.HasIndex(e => e.AccountId, "UQ__Patient__349DA5875F23C293").IsUnique();

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
                .HasConstraintName("FK__Patient__Account__7A672E12");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EA87EC118C");

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
                        .HasConstraintName("FK__ServiceOf__Clini__7B5B524B"),
                    l => l.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ServiceOf__Servi__7C4F7684"),
                    j =>
                    {
                        j.HasKey("ServiceId", "ClinicId").HasName("PK__ServiceO__062FCCC57EAAC23E");
                        j.ToTable("ServiceOfClinic");
                        j.IndexerProperty<Guid>("ServiceId").HasColumnName("ServiceID");
                        j.IndexerProperty<Guid>("ClinicId").HasColumnName("ClinicID");
                    });
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__Slot__0A124A4FD695BACC");

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
            entity.HasKey(e => new { e.SlotId, e.ClinicId }).HasName("PK__SlotOfCl__C9263660D327BF02");

            entity.ToTable("SlotOfClinic");

            entity.Property(e => e.SlotId).HasColumnName("SlotID");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

            entity.HasOne(d => d.Clinic).WithMany(p => p.SlotOfClinics)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SlotOfCli__Clini__7D439ABD");

            entity.HasOne(d => d.Slot).WithMany(p => p.SlotOfClinics)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SlotOfCli__SlotI__7E37BEF6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}