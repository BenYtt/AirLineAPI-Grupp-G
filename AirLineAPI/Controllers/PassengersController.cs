using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirLineAPI.Db_Context;
using AirLineAPI.Services;
using Microsoft.EntityFrameworkCore;
using AirLineAPI.Model;
using Microsoft.AspNetCore.Http;

namespace AirLineAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly IPassengerRepo repo;

        public PassengersController(IPassengerRepo repo)
        {
            this.repo = repo;
        }


        [HttpGet]
        public async Task<ActionResult<Passenger[]>> GetPassenger([FromQuery] bool timeTable) 
        {
            try
            {
                if (repo == null)
                {
                    return NotFound();
                }
                var result = await repo.GetPassengers(timeTable);
                return Ok(result);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failor: {e.Message}");
            }
        
        }

        [Route("{id}")]
        public async Task<ActionResult<Passenger>> GetPassengerById([FromQuery] long passengerID, bool timeTable)
        {
            try
            {
                if (repo == null)
                {
                    return NotFound();
                }
                var result = await repo.GetPassenger(passengerID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $" Database failed {e.Message}");
            }
        }
        
    }
}