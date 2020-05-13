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
    public class RoutesController : ControllerBase
    {
        private readonly IRouteRepository _routeRepository;

        public RoutesController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Route[]>> GetRoutes()
        {
            try
            {
                var result = await _routeRepository.GetRoutes();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Route>> GetRoutesByID(long id)
        {
            try
            {
                var result = await _routeRepository.GetRouteByID(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        [HttpGet]
        [Route("traveltimelessthan={time}")]
        public async Task<ActionResult<Route[]>> GetRoutesByTimeLessThan(int time)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByTimeLessThan(time);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        [HttpGet]
        [Route("traveltimegreaterthan={time}")]
        public async Task<ActionResult<Route[]>> GetRoutesByTimeGreaterThan(int time)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByTimeGreatherThan(time);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        [HttpGet]
        [Route("startdestination={city}")]
        public async Task<ActionResult<Route[]>> GetRoutesByStartDestination(string city)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByStartDestination(city);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }


        [HttpGet]
        [Route("enddestination={city}")]
        public async Task<ActionResult<Route[]>> GetRoutesByEndDestination(string city)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByEndDestination(city);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

    }
}