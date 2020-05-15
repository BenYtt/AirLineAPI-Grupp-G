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

        [HttpGet("{id}")]
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

        [HttpGet("maxtraveltime=h{hours}m{minutes}")]
        public async Task<ActionResult<Route[]>> GetRoutesByTimeLessThan(int hours, int minutes)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByTimeLessThan(hours, minutes);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        [HttpGet("mintraveltime=h{hours}m{minutes}")]
        public async Task<ActionResult<Route[]>> GetRoutesByTimeGreaterThan(int hours, int minutes)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByTimeGreatherThan(hours, minutes);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        [HttpGet("fromcity={city}")]
        public async Task<ActionResult<Route[]>> GetRoutesByStartCity(string city)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByStartCity(city);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }


        [HttpGet("tocity={city}")]
        public async Task<ActionResult<Route[]>> GetRoutesByEndCity(string city)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByEndCity(city);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }


        [HttpGet("tocountry={country}")]
        public async Task<ActionResult<Route[]>> GetRouteByEndCountry(string country)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByEndCountry(country);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        [HttpGet("fromcountry={country}")]
        public async Task<ActionResult<Route[]>> GetRoutesByStartCountry(string country, double includeTime)
        {
            try
            {

                var result = await _routeRepository.GetRoutesByStartCountry(country, includeTime);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }
    }
}