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
    public class TimeTablesController : ControllerBase
    {
        private readonly ITimeTableRepository _repository;

        public TimeTablesController(ITimeTableRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<ActionResult<TimeTable[]>> GetTimeTables(int minMinutes, int maxMinutes, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTables(minMinutes, maxMinutes, includePassengers, includeRoutes);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeTable[]>> GetTimeTableByID(long id, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTableByID(id, includePassengers, includeRoutes);
                if (results == null)
                {
                    return NotFound($"Could not find any timetable with id {id}");
                }
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("startDestination={startDestination}")]
        public async Task<ActionResult<TimeTable[]>> GetTimeTableByStartDestination(string startDestination, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTableByStartDestination(startDestination, includePassengers, includeRoutes);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("endDestination={endDestination}")]
        public async Task<ActionResult<TimeTable[]>> GetTimeTableByEndDestination(string endDestination, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTableByEndDestination(endDestination, includePassengers, includeRoutes);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}