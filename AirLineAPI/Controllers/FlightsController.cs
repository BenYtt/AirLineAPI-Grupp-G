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
    public class FlightsController : ControllerBase
    {
        private readonly IFlightRepository _repository;
        private readonly IMapper _mapper;

        public FlightsController(IFlightRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Flight[]>> GetFlights()
        {
            try
            {
                var results = await _repository.GetFlights();
                var mappedResult = _mapper.Map<FlightDto[]>(results);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight[]>> GetFlightById(long id)
        {
            try
            {
                var results = await _repository.GetFlightByID(id);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("manufacturer={manufacturer}")]
        public async Task<ActionResult<Flight[]>> GetFlightsByManufacturer(string manufacturer)
        {
            try
            {
                var results = await _repository.GetFlightsByManufacturer(manufacturer);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("model/{model}")]
        public async Task<ActionResult<Flight[]>> GetFlightsByModel(string model)
        {
            try
            {
                var results = await _repository.GetFlightsByModel(model);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}