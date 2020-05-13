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
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationRepository _destinationRepository;

        public DestinationsController(IDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Destination>> GetDestinationByID(long id)
        {
            try
            {
                var result = await _destinationRepository.GetDestinationByID(id);
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

        [HttpGet]
        [Route("city={city}")]
        public async Task<ActionResult<Destination>> GetDestinationByName([FromRoute]string city)
        {
            try
            {
                var result = await _destinationRepository.GetDestinationByName(city);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}