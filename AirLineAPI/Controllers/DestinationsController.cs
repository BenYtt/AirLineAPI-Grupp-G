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
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationRepository _destinationRepository;
        private readonly IMapper _mapper;

        public DestinationsController(IDestinationRepository destinationRepository, IMapper mapper)
        {
            _destinationRepository = destinationRepository;
            _mapper = mapper;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestinationByID(long id)
        {
            try
            {
                var result = await _destinationRepository.GetDestinationByID(id);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<Destination[]>> GetDestinations()
        {
            try
            {
                var result = await _destinationRepository.GetDestinations();
                return Ok(result);
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("country={country}")]
        public async Task<ActionResult<Destination[]>> GetDestinationsByCountry(string country)
        {
            try
            {
                var result = await _destinationRepository.GetDestinationsByCountry(country);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("city={city}")]
        public async Task<ActionResult<Destination[]>> GetDestinationsByCity(string city)
        {
            try
            {
                var result = await _destinationRepository.GetDestinationByCity(city);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DestinationDto>> PostEvent(DestinationDto destinationDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Destination>(destinationDto);
                _destinationRepository.Add(mappedEntity);
                if (await _destinationRepository.Save())
                {
                    return Created($"/api/v1.0/Destinations/{mappedEntity.ID}", _mapper.Map<Destination>(mappedEntity));
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