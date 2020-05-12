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
        public async Task<ActionResult<TimeTable[]>> GetTimeTables([FromQuery]bool includePassengers = false, bool includeRoutes = false)
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
        public async Task<ActionResult<TimeTable[]>> GetTimeTableByID(long id, [FromQuery]bool includePassengers = false, [FromQuery]bool includeRoutes = false)
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
    }
}