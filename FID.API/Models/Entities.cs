using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FID.API.Models
{

    public partial class FlightDetail
    {
        public FlightDetail()
        {
        }

        public FlightDetail(string flightNumber)
        {
            FlightNumber = flightNumber;
        }

        public string FlightNumber { get; set; }

        public string AirlineName { get; set; }
        public string Destination { get; set; }

        public DateTime Scheduled_Departure_Time { get; set; }
        public DateTime Estimated_Departure_Time { get; set; }
        public DateTime Actual_Departure_Time { get; set; }
        public string Flight_Status { get; set; }
        public string Departure_Gate { get; set; }


    }

    public class FlightDetailConfiguration : IEntityTypeConfiguration<FlightDetail>
    {
        public void Configure(EntityTypeBuilder<FlightDetail> builder)
        {
            
            builder.HasKey(p => p.FlightNumber);
           

            builder.Property(p => p.AirlineName).IsRequired();
            builder.Property(p => p.Destination);
            builder.Property(p => p.Flight_Status);
            builder.Property(p => p.Departure_Gate);
            

            builder
                .Property(p => p.FlightNumber)
                .IsRequired();                

            builder
                .Property(p => p.Scheduled_Departure_Time)                       
                .ValueGeneratedOnAddOrUpdate();

            builder
                .Property(p => p.Estimated_Departure_Time)                             
                .ValueGeneratedOnAddOrUpdate();

            builder
                .Property(p => p.Actual_Departure_Time)
                .ValueGeneratedOnAddOrUpdate();
        }
    }

    public class FIDContext : DbContext
    {
        public FIDContext(DbContextOptions<FIDContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entity

            modelBuilder
                .ApplyConfiguration(new FlightDetailConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<FlightDetail> FlightDetail { get; set; }
    }

}