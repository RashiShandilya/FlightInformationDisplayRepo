using System;
using FID.API.Models;

namespace FID.API.UnitTests
{
    public static class DbContextExtensions
    {
        public static void Seed(this FIDContext dbContext)
        {
            // Add entities for DbContext instance

            dbContext.FlightDetail.Add(new FlightDetail
            {
                FlightNumber = "C1S234",
                AirlineName = "Mylashiya Airlines",
                Flight_Status = "On Time",
                Destination = "Mylashiya",
                Actual_Departure_Time = DateTime.Now.AddHours(2),
                Scheduled_Departure_Time = DateTime.Now.AddHours(2),
                Estimated_Departure_Time = DateTime.Now.AddHours(2),
                Departure_Gate = ""
                
            });

            dbContext.FlightDetail.Add(new FlightDetail
            {
                FlightNumber = "B3S244",
                AirlineName = "Indian Airlines",
                Flight_Status = "Check In",
                Destination = "India",
                Actual_Departure_Time = DateTime.Now.AddMinutes(10) ,
                Scheduled_Departure_Time = DateTime.Now.AddMinutes(20),
                Estimated_Departure_Time = DateTime.Now.AddHours(10),
                Departure_Gate = ""
            });

            dbContext.FlightDetail.Add(new FlightDetail
            {
                FlightNumber = "F2S2R4",
                AirlineName = "China Airlines",
                Flight_Status = "Boarding",
                Destination = "China",
                Actual_Departure_Time = DateTime.Now.AddMinutes(5),
                Scheduled_Departure_Time = DateTime.Now.AddMinutes(10),
                Estimated_Departure_Time = DateTime.Now.AddHours(10),
                Departure_Gate = "Gate-4"
            });

            dbContext.FlightDetail.Add(new FlightDetail
            {
                FlightNumber = "T2Y2R4",
                AirlineName = "Hongkong Airlines",
                Flight_Status = "Departed",
                Destination = "Hongkong",
                Actual_Departure_Time = DateTime.Now.AddMinutes(5),
                Scheduled_Departure_Time = DateTime.Now.AddMinutes(10),
                Estimated_Departure_Time = DateTime.Now.AddHours(10),
                Departure_Gate = "Gate-5"
            });

            dbContext.FlightDetail.Add(new FlightDetail
            {
                FlightNumber = "L62Y42",
                AirlineName = "Russia Airlines",
                Flight_Status = "Delayed",
                Destination = "Russia",
                Actual_Departure_Time = DateTime.Now.AddHours(2),
                Scheduled_Departure_Time = DateTime.Now,
                Estimated_Departure_Time = DateTime.Now.AddHours(1),
                Departure_Gate = "Gate-7"
            });        



            dbContext.SaveChanges();
        }
    }
}
