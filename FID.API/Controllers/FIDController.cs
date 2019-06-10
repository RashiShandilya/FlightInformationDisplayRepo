using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FID.API.Models;

namespace FID.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class FIDController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly FIDContext DbContext;

        public FIDController(ILogger<FIDController> logger, FIDContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }


        // GET
        // api/v1/fid/FlightDetail

       
        [HttpGet("FlightDetail")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetFlightDetailAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetFlightDetailAsync));

            var response = new PagedResponse<FlightDetail>();

            try
            {               
                var query = DbContext.GetFlightDetail();
               
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                
                response.ItemsCount = await query.CountAsync();
                
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

                response.Message = string.Format("Page {0} of {1}, Total of items: {2}.", pageNumber, response.PageCount, response.ItemsCount);

                Logger?.LogInformation("The flight detail have been retrieved successfully.");
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetFlightDetailAsync), ex);
            }

            return Ok(response);
        }

        // GET
        // api/v1/fid/FlightDetail/C46342
       
        [HttpGet("FlightDetail/{flightNumber}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetFlightDetailAsync(string flightNumber)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetFlightDetailAsync));

            var response = new SingleResponse<FlightDetail>();

            try
            {
                // Get the flight detail by number by flightNumber
                response.Model = await DbContext.GetFlightDetailAsync(new FlightDetail(flightNumber));
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetFlightDetailAsync), ex);
            }

            return Ok(response);
        }

        // POST
        // api/v1/fid/FlightDetail/

        [HttpPost("FlightDetail")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostFlightDetailAsync([FromBody]PostFlightDetailRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostFlightDetailAsync));

            var response = new SingleResponse<FlightDetail>();

            try
            {
                var existingEntity = await DbContext
                    .GetFlightDetailByAirlineNameAsync(new FlightDetail { AirlineName = request.AirlineName });

                if (existingEntity != null)
                    ModelState.AddModelError("AirlineName", "Airline name name already exists");

                if (!ModelState.IsValid)
                    return BadRequest();

                // Create entity from request model
                var entity = request.ToEntity();

                // Add entity to repository
                DbContext.Add(entity);

                // Save entity in database
                await DbContext.SaveChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostFlightDetailAsync), ex);
            }

            return response.ToHttpCreatedResponse();
        }

        // PUT
        // api/v1/fid/FlightDetail/F35443
        
        [HttpPut("FlightDetail/{flightNumber}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutFlightDetailAsync(string flightNumber, [FromBody]PutFlightDetailRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PutFlightDetailAsync));

            var response = new Response();

            try
            {
                // Get flight detail by flightNumber
                var entity = await DbContext.GetFlightDetailAsync(new FlightDetail(flightNumber));

                if (entity == null)
                    return NotFound();

                
                entity.Destination = request.Destination ;
                entity.Departure_Gate = request.Departure_Gate;
                entity.Actual_Departure_Time = request.Actual_Departure_Time;
                entity.Estimated_Departure_Time = request.Estimated_Departure_Time;
                entity.Scheduled_Departure_Time = request.Scheduled_Departure_Time;
                entity.Flight_Status = request.Flight_Status;

               
                DbContext.Update(entity);
               
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutFlightDetailAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // DELETE
        // api/v1/fid/FlightDetail/S63544

        
        [HttpDelete("FlightDetail/{flightNumber}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteFlightDetailAsync(string flightNumber)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteFlightDetailAsync));

            var response = new Response();

            try
            {
                //  Get flight detail by flightNumber
                var entity = await DbContext.GetFlightDetailAsync(new FlightDetail(flightNumber));

               
                if (entity == null)
                    return NotFound();
               
                DbContext.Remove(entity);
               
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(DeleteFlightDetailAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}
