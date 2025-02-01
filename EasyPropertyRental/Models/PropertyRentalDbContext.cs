using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EasyPropertyRental.Models.ViewModels;

namespace EasyPropertyRental.Models;

public partial class PropertyRentalDbContext : DbContext
{
    public PropertyRentalDbContext()
    {
    }

    public PropertyRentalDbContext(DbContextOptions<PropertyRentalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<PropertyManager> PropertyManagers { get; set; }

    public virtual DbSet<PropertyOwner> PropertyOwners { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP\\SQLEXPRESS;Initial Catalog=PropertyRentalDB;User=sa;Password=lasalle;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.ApartmentId).HasName("PK__apartmen__DC51C2EC93EA0B8E");

            entity.ToTable("apartments");

            entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");
            entity.Property(e => e.Bathrooms).HasColumnName("bathrooms");
            entity.Property(e => e.Bedrooms).HasColumnName("bedrooms");
            entity.Property(e => e.BuildingId).HasColumnName("building_id");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.Rent)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("rent");
            entity.Property(e => e.UnitNumber)
                .HasMaxLength(10)
                .HasColumnName("unit_number");

            entity.HasOne(d => d.Building).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__apartment__build__4316F928");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__appointm__A50828FC335BED9C");

            entity.ToTable("appointments");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentDate)
                .HasColumnType("datetime")
                .HasColumnName("appointment_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PmId).HasColumnName("pm_id");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.Pm).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PmId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__appointme__pm_id__4BAC3F29");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__appointme__tenan__4AB81AF0");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.BuildingId).HasName("PK__building__9C9FBF7F17E45386");

            entity.ToTable("buildings");

            entity.Property(e => e.BuildingId).HasColumnName("building_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PmId).HasColumnName("pm_id");

            entity.HasOne(d => d.Pm).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.PmId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__buildings__pm_id__3F466844");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__messages__0BBF6EE6F1BBF413");

            entity.ToTable("messages");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.ReceiverType)
                .HasMaxLength(20)
                .HasColumnName("receiver_type");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.SenderType)
                .HasMaxLength(20)
                .HasColumnName("sender_type");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("sent_at");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__receiv__5AEE82B9");

            entity.HasOne(d => d.ReceiverNavigation).WithMany(p => p.MessageReceiverNavigations)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__receiv__5BE2A6F2");

            entity.HasOne(d => d.Receiver1).WithMany(p => p.MessageReceiver1s)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__receiv__59FA5E80");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__sender__5812160E");

            entity.HasOne(d => d.SenderNavigation).WithMany(p => p.MessageSenderNavigations)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__sender__59063A47");

            entity.HasOne(d => d.Sender1).WithMany(p => p.MessageSender1s)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__sender__571DF1D5");
        });

        modelBuilder.Entity<PropertyManager>(entity =>
        {
            entity.HasKey(e => e.PmId).HasName("PK__property__26B1033612BE5E19");

            entity.ToTable("propertyManagers");

            entity.HasIndex(e => e.Email, "UQ__property__AB6E6164FD60D7AF").IsUnique();

            entity.Property(e => e.PmId).HasColumnName("pm_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.PoId).HasColumnName("po_id");

            entity.HasOne(d => d.Po).WithMany(p => p.PropertyManagers)
                .HasForeignKey(d => d.PoId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__propertyM__po_id__3C69FB99");
        });

        modelBuilder.Entity<PropertyOwner>(entity =>
        {
            entity.HasKey(e => e.PoId).HasName("PK__property__368DA7F071EEF4CF");

            entity.ToTable("propertyOwners");

            entity.HasIndex(e => e.Email, "UQ__property__AB6E61649182340E").IsUnique();

            entity.Property(e => e.PoId).HasColumnName("po_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.TenantId).HasName("PK__tenants__D6F29F3E48711940");

            entity.ToTable("tenants");

            entity.HasIndex(e => e.Email, "UQ__tenants__AB6E6164130DA31B").IsUnique();

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MoveInDate).HasColumnName("move_in_date");
            entity.Property(e => e.MoveOutDate).HasColumnName("move_out_date");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.Property(e => e.PmId).HasColumnName("pm_id");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Tenants)
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__tenants__apartme__46E78A0C");

            entity.HasOne(d => d.PropertyManager).WithMany(p => p.Tenants)
                .HasForeignKey(d => d.PmId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__tenants__pm_id__47E78A0D");


        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<EasyPropertyRental.Models.ViewModels.LoginViewModel> LoginViewModel { get; set; } = default!;

public DbSet<EasyPropertyRental.Models.ViewModels.RegistrationViewModel> RegistrationViewModel { get; set; } = default!;
}
