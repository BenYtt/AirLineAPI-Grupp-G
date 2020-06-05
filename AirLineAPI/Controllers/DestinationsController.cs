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
using Microsoft.AspNetCore.Authorization;
using AirLineAPI.Filters;

namespace AirLineAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class DestinationsController : HateoasControllerBase
    {
        private readonly IDestinationRepository _destinationRepository;
        private readonly IMapper _mapper;

        public DestinationsController(IDestinationRepository destinationRepository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _destinationRepository = destinationRepository;
            _mapper = mapper;
        }

        //api/v1.0/destinations     Get all destinations
        [HttpGet(Name = "GetDestinations")]
        public async Task<ActionResult<Destination[]>> GetDestinations()
        {
            try
            {
                var result = await _destinationRepository.GetDestinations();
                var destinationresult = _mapper.Map<DestinationDto[]>(result).Select(m => HateoasMainLinksDestinations(m));
                if (result == null)
                {
                    return NotFound($"Could not find any destinations");
                }
                return Ok(destinationresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //api/v1.0/destination/{id}      Get destination by id
        [HttpGet("{id}", Name = "GetDestinationById")]
        public async Task<ActionResult<DestinationDto>> GetDestinationById(int id)
        {
            try
            {
                var result = await _destinationRepository.GetDestinationById(id);
                var destinationresult = _mapper.Map<DestinationDto>(result);
                if (result == null)
                {
                    return NotFound($"Could not find any destination with id {id}");
                }
                return Ok(HateoasMainLinksDestinations(destinationresult));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //api/v1.0/destinations/country=Sweden      Get destination by Country
        [HttpGet("country={country}", Name = "GetDestinationByCountry")]
        public async Task<ActionResult<Destination[]>> GetDestinationsByCountry(string country)
        {
            try
            {
                var result = await _destinationRepository.GetDestinationsByCountry(country);
                var destinationresult = _mapper.Map<DestinationDto[]>(result).Select(m => HateoasMainLinksDestinations(m));
                if (result == null)
                {
                    return NotFound($"Could not find any destination with name {country}");
                }
                return Ok(destinationresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //api/v1.0/destinations/city=stockholm      Get destination by City
        [HttpGet("city={city}", Name = "GetDestinationByCity")]
        public async Task<ActionResult<DestinationDto[]>> GetDestinationsByCity(string city)
        {
            try
            {
                var result = await _destinationRepository.GetDestinationByCity(city);
                var destinationresult = _mapper.Map<DestinationDto[]>(result).Select(m => HateoasMainLinksDestinations(m));
                if (result == null)
                {
                    return NotFound($"Could not find any destination with city name {city}");
                }
                return Ok(destinationresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        // {"city": "name",
        //"country": "name"}
        [HttpPost]
        public async Task<ActionResult<DestinationDto>> PostEvent(DestinationDto destinationDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Destination>(destinationDto);
                _destinationRepository.Add(mappedEntity);
                if (await _destinationRepository.Save())
                {
                    return Created($"/api/v1.0/Destinations/{mappedEntity.Id}", _mapper.Map<Destination>(mappedEntity));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }

        //https:/localhost:44333/api/v1.0/destinations/
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDestination(int id, DestinationDto destinationDto)
        {
            try
            {
                var oldDestination = await _destinationRepository.GetDestinationById(id);
                if (oldDestination == null)
                {
                    return NotFound($"Could not find destination with id {id}");
                }

                var newDestination = _mapper.Map(destinationDto, oldDestination);
                _destinationRepository.Update(newDestination);
                if (await _destinationRepository.Save())
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }

        //https:/localhost:44333/api/v1.0/destinations/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDestination(int id)
        {
            try
            {
                var OldDestination = await _destinationRepository.GetDestinationById(id);
                if (OldDestination == null)
                {
                    return NotFound($"Counld not find destination with id {id}");
                }
                _destinationRepository.Delete(OldDestination);
                if (await _destinationRepository.Save())
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }
    }
}