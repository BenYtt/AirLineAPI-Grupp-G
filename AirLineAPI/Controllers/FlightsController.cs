﻿using System;
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
using System.Data.OleDb;

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
        public async Task<ActionResult<FlightDto[]>> GetFlights()
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
        public async Task<ActionResult<FlightDto>> GetFlightById(long id)
        {
            try
            {
                var results = await _repository.GetFlightByID(id);
                var mappedResult = _mapper.Map<FlightDto>(results);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("manufacturer={manufacturer}")]
        public async Task<ActionResult<FlightDto[]>> GetFlightsByManufacturer(string manufacturer)
        {
            try
            {
                var results = await _repository.GetFlightsByManufacturer(manufacturer);
                var mappedResult = _mapper.Map<FlightDto[]>(results);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("model/{model}")]
        public async Task<ActionResult<FlightDto[]>> GetFlightsByModel(string model)
        {
            try
            {
                var results = await _repository.GetFlightsByModel(model);
                var mappedResult = _mapper.Map<FlightDto[]>(results);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //POST: api/v1.0/flights                                 POST Flight
        [HttpPost]
        public async Task<ActionResult<FlightDto>> PostEvent([FromBody]FlightDto flightDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Flight>(flightDto);
                _repository.Add(mappedEntity);
                if (await _repository.Save())
                {
                    return Created($"/api/v1.0/Flights/{mappedEntity.ID}", _mapper.Map<Flight>(mappedEntity));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }

        //PUT: api/v1.0/flights                                 PUT Flight
        [HttpPut("{id}")]
        public async Task<ActionResult<FlightDto>> PutEvent(long id, [FromBody]FlightDto flightDto)
        {
            try
            {
                var oldFlight = await _repository.GetFlightByID(id);

                if (oldFlight == null)
                {
                    return NotFound($"Couldn't find any flight with id: {id}");
                }

                var newFlight = _mapper.Map(flightDto, oldFlight);
                _repository.Update(newFlight);
               
                if (await _repository.Save())
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(long id)
        {
            try
            {
                var oldFlight = await _repository.GetFlightByID(id);

                if (oldFlight == null)
                {
                    return NotFound($"Couldn't find any flight with id: {id}");
                }

                _repository.Delete(oldFlight);

                if (await _repository.Save())
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