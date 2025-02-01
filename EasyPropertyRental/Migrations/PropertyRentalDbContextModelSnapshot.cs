﻿// <auto-generated />
using System;
using EasyPropertyRental.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyPropertyRental.Migrations
{
    [DbContext(typeof(PropertyRentalDbContext))]
    partial class PropertyRentalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EasyPropertyRental.Models.Apartment", b =>
                {
                    b.Property<int>("ApartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("apartment_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApartmentId"));

                    b.Property<int?>("Bathrooms")
                        .HasColumnType("int")
                        .HasColumnName("bathrooms");

                    b.Property<int?>("Bedrooms")
                        .HasColumnType("int")
                        .HasColumnName("bedrooms");

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int")
                        .HasColumnName("building_id");

                    b.Property<int?>("Floor")
                        .HasColumnType("int")
                        .HasColumnName("floor");

                    b.Property<bool?>("IsAvailable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnName("is_available");

                    b.Property<decimal?>("Rent")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("rent");

                    b.Property<string>("UnitNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("unit_number");

                    b.HasKey("ApartmentId")
                        .HasName("PK__apartmen__DC51C2EC93EA0B8E");

                    b.HasIndex("BuildingId");

                    b.ToTable("apartments", (string)null);
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("appointment_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime")
                        .HasColumnName("appointment_date");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("notes");

                    b.Property<int?>("PmId")
                        .HasColumnType("int")
                        .HasColumnName("pm_id");

                    b.Property<int?>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.HasKey("AppointmentId")
                        .HasName("PK__appointm__A50828FC335BED9C");

                    b.HasIndex("PmId");

                    b.HasIndex("TenantId");

                    b.ToTable("appointments", (string)null);
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("building_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BuildingId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<int?>("PmId")
                        .HasColumnType("int")
                        .HasColumnName("pm_id");

                    b.HasKey("BuildingId")
                        .HasName("PK__building__9C9FBF7F17E45386");

                    b.HasIndex("PmId");

                    b.ToTable("buildings", (string)null);
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("message_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int")
                        .HasColumnName("receiver_id");

                    b.Property<string>("ReceiverType")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("receiver_type");

                    b.Property<int>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("sender_id");

                    b.Property<string>("SenderType")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("sender_type");

                    b.Property<DateTime?>("SentAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("sent_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("MessageId")
                        .HasName("PK__messages__0BBF6EE6F1BBF413");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("EasyPropertyRental.Models.PropertyManager", b =>
                {
                    b.Property<int>("PmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pm_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PmId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone");

                    b.Property<int?>("PoId")
                        .HasColumnType("int")
                        .HasColumnName("po_id");

                    b.HasKey("PmId")
                        .HasName("PK__property__26B1033612BE5E19");

                    b.HasIndex("PoId");

                    b.HasIndex(new[] { "Email" }, "UQ__property__AB6E6164FD60D7AF")
                        .IsUnique();

                    b.ToTable("propertyManagers", (string)null);
                });

            modelBuilder.Entity("EasyPropertyRental.Models.PropertyOwner", b =>
                {
                    b.Property<int>("PoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("po_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PoId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("password");

                    b.HasKey("PoId")
                        .HasName("PK__property__368DA7F071EEF4CF");

                    b.HasIndex(new[] { "Email" }, "UQ__property__AB6E61649182340E")
                        .IsUnique();

                    b.ToTable("propertyOwners", (string)null);
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Tenant", b =>
                {
                    b.Property<int>("TenantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TenantId"));

                    b.Property<int?>("ApartmentId")
                        .HasColumnType("int")
                        .HasColumnName("apartment_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("last_name");

                    b.Property<DateOnly?>("MoveInDate")
                        .HasColumnType("date")
                        .HasColumnName("move_in_date");

                    b.Property<DateOnly?>("MoveOutDate")
                        .HasColumnType("date")
                        .HasColumnName("move_out_date");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone");

                    b.Property<int?>("PmId")
                        .HasColumnType("int")
                        .HasColumnName("pm_id");

                    b.HasKey("TenantId")
                        .HasName("PK__tenants__D6F29F3E48711940");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("PmId");

                    b.HasIndex(new[] { "Email" }, "UQ__tenants__AB6E6164130DA31B")
                        .IsUnique();

                    b.ToTable("tenants", (string)null);
                });

            modelBuilder.Entity("EasyPropertyRental.Models.ViewModels.LoginViewModel", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("LoginViewModel");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.ViewModels.RegistrationViewModel", b =>
                {
                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Email");

                    b.ToTable("RegistrationViewModel");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Apartment", b =>
                {
                    b.HasOne("EasyPropertyRental.Models.Building", "Building")
                        .WithMany("Apartments")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__apartment__build__4316F928");

                    b.Navigation("Building");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Appointment", b =>
                {
                    b.HasOne("EasyPropertyRental.Models.PropertyManager", "Pm")
                        .WithMany("Appointments")
                        .HasForeignKey("PmId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__appointme__pm_id__4BAC3F29");

                    b.HasOne("EasyPropertyRental.Models.Tenant", "Tenant")
                        .WithMany("Appointments")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__appointme__tenan__4AB81AF0");

                    b.Navigation("Pm");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Building", b =>
                {
                    b.HasOne("EasyPropertyRental.Models.PropertyManager", "Pm")
                        .WithMany("Buildings")
                        .HasForeignKey("PmId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__buildings__pm_id__3F466844");

                    b.Navigation("Pm");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Message", b =>
                {
                    b.HasOne("EasyPropertyRental.Models.PropertyManager", "Receiver")
                        .WithMany("MessageReceivers")
                        .HasForeignKey("ReceiverId")
                        .IsRequired()
                        .HasConstraintName("FK__messages__receiv__5AEE82B9");

                    b.HasOne("EasyPropertyRental.Models.PropertyOwner", "ReceiverNavigation")
                        .WithMany("MessageReceiverNavigations")
                        .HasForeignKey("ReceiverId")
                        .IsRequired()
                        .HasConstraintName("FK__messages__receiv__5BE2A6F2");

                    b.HasOne("EasyPropertyRental.Models.Tenant", "Receiver1")
                        .WithMany("MessageReceiver1s")
                        .HasForeignKey("ReceiverId")
                        .IsRequired()
                        .HasConstraintName("FK__messages__receiv__59FA5E80");

                    b.HasOne("EasyPropertyRental.Models.PropertyManager", "Sender")
                        .WithMany("MessageSenders")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("FK__messages__sender__5812160E");

                    b.HasOne("EasyPropertyRental.Models.PropertyOwner", "SenderNavigation")
                        .WithMany("MessageSenderNavigations")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("FK__messages__sender__59063A47");

                    b.HasOne("EasyPropertyRental.Models.Tenant", "Sender1")
                        .WithMany("MessageSender1s")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("FK__messages__sender__571DF1D5");

                    b.Navigation("Receiver");

                    b.Navigation("Receiver1");

                    b.Navigation("ReceiverNavigation");

                    b.Navigation("Sender");

                    b.Navigation("Sender1");

                    b.Navigation("SenderNavigation");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.PropertyManager", b =>
                {
                    b.HasOne("EasyPropertyRental.Models.PropertyOwner", "Po")
                        .WithMany("PropertyManagers")
                        .HasForeignKey("PoId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__propertyM__po_id__3C69FB99");

                    b.Navigation("Po");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Tenant", b =>
                {
                    b.HasOne("EasyPropertyRental.Models.Apartment", "Apartment")
                        .WithMany("Tenants")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__tenants__apartme__46E78A0C");

                    b.HasOne("EasyPropertyRental.Models.PropertyManager", "PropertyManager")
                        .WithMany("Tenants")
                        .HasForeignKey("PmId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__tenants__pm_id__47E78A0D");

                    b.Navigation("Apartment");

                    b.Navigation("PropertyManager");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Apartment", b =>
                {
                    b.Navigation("Tenants");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Building", b =>
                {
                    b.Navigation("Apartments");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.PropertyManager", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Buildings");

                    b.Navigation("MessageReceivers");

                    b.Navigation("MessageSenders");

                    b.Navigation("Tenants");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.PropertyOwner", b =>
                {
                    b.Navigation("MessageReceiverNavigations");

                    b.Navigation("MessageSenderNavigations");

                    b.Navigation("PropertyManagers");
                });

            modelBuilder.Entity("EasyPropertyRental.Models.Tenant", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("MessageReceiver1s");

                    b.Navigation("MessageSender1s");
                });
#pragma warning restore 612, 618
        }
    }
}
