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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AirLineAPI.Filters;

namespace AirLineAPI.Controllers
{
    //[ApiKeyAuth]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class RoutesController : HateoasControllerBase
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;

        public RoutesController(IRouteRepository routeRepository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
        }

        //Get: api/v1.0/Routes/                                 Get Routes
        [HttpGet(Name = "GetRoutes")]
        public async Task<ActionResult<RouteDto[]>> GetRoutes(int minMinutes, int maxMinutes)
        {
            try
            {
                var result = await _routeRepository.GetRoutes(minMinutes, maxMinutes);
                var passengerresult = _mapper.Map<RouteDto[]>(result).Select(m => HateoasMainLinksRoute(m));
                return Ok(passengerresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        //Get: api/v1.0/Routes/{id}                                 Get Route by id
        [HttpGet("{id}", Name = "GetRouteById")]
        public async Task<ActionResult<RouteDto>> GetRoutesById(int id)
        {
            try
            {
                var result = await _routeRepository.GetRouteById(id);
                var routeResult = _mapper.Map<RouteDto>(result);

                if (result == null)
                {
                    return NotFound($"There is no route with id:{id}");
                }

                return Ok(HateoasMainLinksRoute(routeResult));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        //Get: api/v1.0/Routes/fromcity=<city>                                 Get Route by fromcity
        [HttpGet("fromCity={fromCity}", Name = "GetRouteByStartCity")]
        public async Task<ActionResult<RouteDto>> GetRoutesByStartCity(string fromCity)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByStartCity(fromCity);
                var passengerresult = _mapper.Map<RouteDto>(result);
                return Ok(HateoasMainLinksRoute(passengerresult));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        //Get: api/v1.0/Routes/tocity=<city>                                 Get Route to city
        [HttpGet("tocity={toCity}", Name = "GetRoutesByEndCity")]
        public async Task<ActionResult<RouteDto[]>> GetRoutesByEndCity(string toCity)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByEndCity(toCity);
                var passengerresult = _mapper.Map<RouteDto[]>(result).Select(m => HateoasMainLinksRoute(m));
                return Ok(passengerresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        //Get: api/v1.0/Routes/Endccountry=<country>                                 Get Route by country
        [HttpGet("Endcountry={EndCountry}", Name = "GetRouteByEndCountry")]
        public async Task<ActionResult<RouteDto[]>> GetRouteByEndCountry(string country)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByEndCountry(country);
                var passengerresult = _mapper.Map<RouteDto[]>(result).Select(m => HateoasMainLinksRoute(m));
                return Ok(passengerresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        //Get: api/v1.0/Routes/fromcountry=<city>                                 Get Route route by city
        [HttpGet("Fromcountry={FromCountry}", Name = "GetRoutesByStartCountry")]
        public async Task<ActionResult<RouteDto[]>> GetRoutesByStartCountry(string country, double includeTime)
        {
            try
            {
                var result = await _routeRepository.GetRoutesByStartCountry(country, includeTime);
                var passengerresult = _mapper.Map<RouteDto[]>(result).Select(m => HateoasMainLinksRoute(m));
                return Ok(passengerresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
        }

        //Put: api/v1.0/Routes/{id}                                 Put Route
        [HttpPut("{id}")]
        public async Task<ActionResult<RouteDto>> PutRoute(int id, [FromBody] RouteDto routeDto)
        {
            try
            {
                var oldRoute = await _routeRepository.GetRouteById(id);
                if (oldRoute == null)
                {
                    return NotFound($"there is no routes with id:{id}");
                }
                var newRoute = _mapper.Map(routeDto, oldRoute);
                _routeRepository.Update(newRoute);

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

        //Delete: api/v1.0/Routes/{id}                                 Delete Route
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoute(int id)
        {
            try
            {
                var oldRoute = await _routeRepository.GetRouteById(id);
                if (oldRoute == null)
                {
                    return NotFound($"there is no routes with id:{id}");
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