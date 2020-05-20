using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirLineAPI.Db_Context;
using AirLineAPI.Services;
using AirLineAPI.Model;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using AirLineAPI.Dto;

namespace AirLineAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;

        public RoutesController(IRouteRepository routeRepository, IMapper mapper)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Route[]>> GetRoutes(int minMinutes, int maxMinutes)
        {
            try
            {
                var result = await _routeRepository.GetRoutes(minMinutes, maxMinutes);
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
                var mappedResult = _mapper.Map<RouteDto>(result);

                if (mappedResult == null)
                {
                    return NotFound($"There is no route with id:{id}");
                }

                return Ok(mappedResult);
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

        [HttpDelete("{routeid}")]
        public async Task<ActionResult> DeleteRoute(long routeid)
        {
            try
            {
                var oldRoute = await _routeRepository.GetRouteByID(routeid);
                if (oldRoute == null)
                {
                    return NotFound($"There is no routes with id:{routeid}");
                }

                _routeRepository.Delete(oldRoute);

                if (await _routeRepository.Save())
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
            return BadRequest();
        }
    }
}