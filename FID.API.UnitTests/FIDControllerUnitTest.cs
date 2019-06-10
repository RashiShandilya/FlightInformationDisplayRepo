using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FID.API.Controllers;
using FID.API.Models;
using Xunit;

namespace FID.API.UnitTests
{
    public class FIDControllerUnitTest
    {
        [Fact]
        public async Task TestGetAllFlightDetailAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetFIDDbContext(nameof(TestGetAllFlightDetailAsync));
            var controller = new FIDController(null, dbContext);

            // Act
            var response = await controller.GetFlightDetailAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<FlightDetail>;

            dbContext.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]

        public async Task TestGetFlightDetailAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetFIDDbContext(nameof(TestGetFlightDetailAsync));
            var controller = new FIDController(null, dbContext);
            var flightNumber = "C1S234";

            // Act
            var response = await controller.GetFlightDetailAsync(flightNumber) as ObjectResult;
            var value = response.Value as ISingleResponse<FlightDetail>;

            dbContext.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestPostFlightDetailAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetFIDDbContext(nameof(TestPostFlightDetailAsync));
            var controller = new FIDController(null, dbContext);
            var request = new PostFlightDetailRequest
            {
                FlightNumber = "A1002B1",
                AirlineName = "Singapore Airlines",
                Destination = "Singapore",
                Departure_Gate = "Gate-1",
                Flight_Status = "Departed", 
                Scheduled_Departure_Time = DateTime.Now.AddMinutes(30),
                Estimated_Departure_Time = DateTime.Now.AddMinutes(40),
                Actual_Departure_Time = DateTime.Now.AddMinutes(50),
            };

            // Act
            var response = await controller.PostFlightDetailAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<FlightDetail>;

            dbContext.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestPutFlightDetailAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetFIDDbContext(nameof(TestPutFlightDetailAsync));
            var controller = new FIDController(null, dbContext);
            var flightNumber = "T2Y2R4";
            var request = new PutFlightDetailRequest
            {
                 Destination = "Canada",
                 Scheduled_Departure_Time = DateTime.Now,
                Estimated_Departure_Time = DateTime.Now,
                Actual_Departure_Time = DateTime.Now,
                Flight_Status= "Cancel",
                Departure_Gate = ""
       
    };

            // Act
            var response = await controller.PutFlightDetailAsync(flightNumber, request) as ObjectResult;
            var value = response.Value as IResponse;

            dbContext.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestDeleteFlightDetailAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetFIDDbContext(nameof(TestDeleteFlightDetailAsync));
            var controller = new FIDController(null, dbContext);
            var flightNumber = "L62Y42";

            // Act
            var response = await controller.DeleteFlightDetailAsync(flightNumber) as ObjectResult;
            var value = response.Value as IResponse;

            dbContext.Dispose();

            // Assert
            Assert.False(value.DidError);
        }


    }
}
