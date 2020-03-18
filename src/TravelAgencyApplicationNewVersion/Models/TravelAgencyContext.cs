using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TravelAgencyApplicationNewVersion.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TravelAgencyApplicationNewVersion.Models
{
    public partial class TravelAgencyContext : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-V5GCG2NR;Database=TravelAgency;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //// ignore a type that is not mapped to a database table
            //modelBuilder.Ignore<SearchIndexViewModel>();

            //// ignore a property that is not mapped to a database column
            //modelBuilder.Entity<SearchIndexViewModel>()
            //    .Ignore(p => p.DepartureCities);


            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(max)");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.SeatId)
                    .HasName("PK_Flight");

                entity.Property(e => e.SeatId).HasColumnName("SeatID");

                entity.Property(e => e.Airline)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.ArrivalCity)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.ArrivalTime).HasColumnType("datetime");

                entity.Property(e => e.DepartureCity)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.DepartureTime).HasColumnType("datetime");

                entity.Property(e => e.FlightId).HasColumnName("FlightID");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PK_Hotel");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.HotelName)
                    .IsRequired()
                    .HasColumnType("varchar(max)");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Reservation_Customer");
            });

            modelBuilder.Entity<ReservationDetails>(entity =>
            {
                entity.Property(e => e.ReservationDetailsId).HasColumnName("ReservationDetailsID");

                entity.Property(e => e.ArrivalTime).HasColumnType("datetime");

                entity.Property(e => e.BookingDate).HasColumnType("date");

                entity.Property(e => e.DepartureTime).HasColumnType("datetime");

                entity.Property(e => e.InvoiceExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.SeatId).HasColumnName("SeatID");

                entity.Property(e => e.Email)                    
                    .HasColumnType("nvarchar(256)");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.ReservationDetails)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ReservationDetails_Flight");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.ReservationDetails)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_ReservationDetails_Hotel");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.ReservationDetails)
                    .HasForeignKey(d => d.SeatId)
                    .HasConstraintName("FK_ReservationDetails_Flight1");
            });
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Flight> Flight { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<ReservationDetails> ReservationDetails { get; set; }




    }
}