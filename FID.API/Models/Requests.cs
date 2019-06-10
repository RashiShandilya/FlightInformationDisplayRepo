using System;
using System.ComponentModel.DataAnnotations;

namespace FID.API.Models
{
#pragma warning disable CS1591
    public class PostFlightDetailRequest
    {
        [Key]
        [Required]
        public string FlightNumber { get; set; }

        [Required]       
        public string AirlineName { get; set; }
       
        public string Destination { get; set; }

        public DateTime Scheduled_Departure_Time { get; set; }
        public DateTime Estimated_Departure_Time { get; set; }
        public DateTime Actual_Departure_Time { get; set; }
        public string Flight_Status { get; set; }
        public string Departure_Gate { get; set; }
    }

    public class PutFlightDetailRequest
    {
        public string Destination { get; set; }

        public DateTime Scheduled_Departure_Time { get; set; }
        public DateTime Estimated_Departure_Time { get; set; }
        public DateTime Actual_Departure_Time { get; set; }
        public string Flight_Status { get; set; }
        public string Departure_Gate { get; set; }
    }

    public static class Extensions
    {
        public static FlightDetail ToEntity(this PostFlightDetailRequest request)
            => new FlightDetail
            {
                FlightNumber = request.FlightNumber,
                AirlineName = request.AirlineName,
                Destination = request.Destination,
                Scheduled_Departure_Time= request.Scheduled_Departure_Time,
                Estimated_Departure_Time= request.Estimated_Departure_Time,
                Actual_Departure_Time= request.Actual_Departure_Time,
                Flight_Status = request.Flight_Status,
                Departure_Gate = request.Departure_Gate
               
            };
    }
#pragma warning restore CS1591
}
