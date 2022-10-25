using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models.EF
{
    public partial class AirlineAppDBContext : DbContext
    {
        public AirlineAppDBContext()
        {
        }

        public AirlineAppDBContext(DbContextOptions<AirlineAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookFlight> BookFlights { get; set; } = null!;
        public virtual DbSet<BookTicket> BookTickets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=DESKTOP-0CD2UCE\\SQLEXPRESS;database=AirlineAppDB;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookFlight>(entity =>
            {
                entity.HasKey(e => e.FlightNo)
                    .HasName("PK__BookFlig__0E01AFB9F65F77A9");

                entity.ToTable("BookFlight");

                entity.Property(e => e.FlightNo)
                    .ValueGeneratedNever()
                    .HasColumnName("flightNo");

                entity.Property(e => e.Destination)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("destination");

                entity.Property(e => e.Fare).HasColumnName("fare");

                entity.Property(e => e.Source)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("source");

                entity.Property(e => e.TotalSeats).HasColumnName("totalSeats");
            });

            modelBuilder.Entity<BookTicket>(entity =>
            {
                entity.HasKey(e => e.PassengerId)
                    .HasName("PK__BookTick__D9AD61D3F7E29DCE");

                entity.ToTable("BookTicket");

                entity.Property(e => e.PassengerId)
                    .ValueGeneratedNever()
                    .HasColumnName("passengerId");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerFistName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("passengerFistName");

                entity.Property(e => e.PassengerLastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("passengerLastName");

                entity.HasOne(d => d.FlightNoNavigation)
                    .WithMany(p => p.BookTickets)
                    .HasForeignKey(d => d.FlightNo)
                    .HasConstraintName("FK__BookTicke__Fligh__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
