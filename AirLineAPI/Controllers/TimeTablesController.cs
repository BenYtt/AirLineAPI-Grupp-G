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
        public async Task<ActionResult<TimeTable[]>> GetTimeTables(bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTables(includePassengers, includeRoutes);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TimeTable[]>> GetTimeTableByID(long id, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTableByID(id, includePassengers, includeRoutes);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet]
        [Route("startDestination={startDestination}")]
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

        [HttpGet]
        [Route("endDestination={endDestination}")]
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

        [HttpGet]
        [Route("mintraveltime=h{hours}m{minutes}")]
        public async Task<ActionResult<TimeTable[]>> GetTimeTablesByIntervalGreaterThan(int hours, int minutes, [FromQuery]bool includePassengers = false, bool includeRoutes = false)
        {
            
            try
            {
                TimeSpan minTime = new TimeSpan(0, hours, minutes, 0);
                var results = await _repository.GetTimeTablesByIntervalGreaterThan(minTime, includePassengers, includeRoutes);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

    }
}