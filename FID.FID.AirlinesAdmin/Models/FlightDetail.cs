using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FID.AirlinesAdmin.Models
{
    public class FlightDetail
    {
        public string FlightNumber { get; set; }
        public string AirlineName { get; set; }
        public string Destination { get; set; }

        public DateTime Scheduled_Departure_Time { get; set; }
        public DateTime Estimated_Departure_Time { get; set; }
        public DateTime Actual_Departure_Time { get; set; }
        public string Flight_Status { get; set; }
        public string Departure_Gate { get; set; }
    }
}