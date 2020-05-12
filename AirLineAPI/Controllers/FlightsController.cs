using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirLineAPI.Db_Context;
using AirLineAPI.Services;
using AirLineAPI.Model;
using Microsoft.AspNetCore.Http;

namespace AirLineAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightRepository _repository;

        public FlightsController(IFlightRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Flight[]>> GetFlights()
        {
            try
            {
                var results = await _repository.GetFlights();
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<Flight[]>> GetFlightById(long id)
        {
            try
            {
                var results = await _repository.GetFlightByID(id);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

    }
}