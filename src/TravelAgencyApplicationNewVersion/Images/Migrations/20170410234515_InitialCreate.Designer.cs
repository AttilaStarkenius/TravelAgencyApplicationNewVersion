using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TravelAgencyApplicationNewVersion.Models;

namespace TravelAgencyApplicationNewVersion.Migrations
{
    [DbContext(typeof(TravelAgencyContext))]
    [Migration("20170410234515_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TravelAgencyApplicationNewVersion.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CustomerID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("Member");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("TravelAgencyApplicationNewVersion.Models.Flight", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SeatID");

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ArrivalCity")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime");

                    b.Property<string>("DepartureCity")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("FirstClass");

                    b.Property<int>("FlightId")
                        .HasColumnName("FlightID");

                    b.Property<double>("FlightPrice");

                    b.HasKey("SeatId")
                        .HasName("PK_Flight");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("TravelAgencyApplicationNewVersion.Models.Hotel", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoomID");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnName("HotelID");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<double>("Price");

                    b.HasKey("RoomId")
                        .HasName("PK_Hotel");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("TravelAgencyApplicationNewVersion.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ReservationID");

                    b.Property<int>("CustomerId")
                        .HasColumnName("CustomerID");

                    b.Property<double>("TotalPrice");

                    b.HasKey("ReservationId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("TravelAgencyApplicationNewVersion.Models.ReservationDetails", b =>
                {
                    b.Property<int>("ReservationDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ReservationDetailsID");

                    b.Property<DateTime?>("ArrivalTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("date");

                    b.Property<bool?>("Breakfast");

                    b.Property<DateTime?>("DepartureTime")
                        .HasColumnType("datetime");

                    b.Property<bool?>("FirstClass");

                    b.Property<bool>("Insurance");

                    b.Property<DateTime>("InvoiceExpiryDate")
                        .HasColumnType("datetime");

                    b.Property<double>("Price");

                    b.Property<int>("ReservationId")
                        .HasColumnName("ReservationID");

                    b.Property<int?>("RoomId")
                        .HasColumnName("RoomID");

                    b.Property<int?>("SeatId")
                        .HasColumnName("SeatID");

                    b.HasKey("ReservationDetailsId");

                    b.HasIndex("ReservationId");

                    b.HasIndex("RoomId");

                    b.HasIndex("SeatId");

                    b.ToTable("ReservationDetails");
                });

            modelBuilder.Entity("TravelAgencyApplicationNewVersion.Models.Reservation", b =>
                {
                    b.HasOne("TravelAgencyApplicationNewVersion.Models.Customer", "Customer")
                        .WithMany("Reservation")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Reservation_Customer");
                });

            modelBuilder.Entity("TravelAgencyApplicationNewVersion.Models.ReservationDetails", b =>
                {
                    b.HasOne("TravelAgencyApplicationNewVersion.Models.Reservation", "Reservation")
                        .WithMany("ReservationDetails")
                        .HasForeignKey("ReservationId")
                        .HasConstraintName("FK_ReservationDetails_Flight");

                    b.HasOne("TravelAgencyApplicationNewVersion.Models.Hotel", "Room")
                        .WithMany("ReservationDetails")
                        .HasForeignKey("RoomId")
                        .HasConstraintName("FK_ReservationDetails_Hotel");

                    b.HasOne("TravelAgencyApplicationNewVersion.Models.Flight", "Seat")
                        .WithMany("ReservationDetails")
                        .HasForeignKey("SeatId")
                        .HasConstraintName("FK_ReservationDetails_Flight1");
                });
        }
    }
}
