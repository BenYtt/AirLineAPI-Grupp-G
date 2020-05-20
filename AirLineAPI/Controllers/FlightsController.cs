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
using System.Data.OleDb;


namespace AirLineAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    public class FlightsController : ControllerBase
    {

        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;

        public FlightsController(IFlightRepository flightRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        //GET: api/v1.0/flights                                 Get Flights
        [HttpGet]
        public async Task<ActionResult<FlightDto[]>> GetFlights()
        {
            try
            {

                var results = await _flightRepository.GetFlights();
                var mappedResult = _mapper.Map<FlightDto[]>(results);

                if (results == null)
                {
                    return NotFound("Could not find any flights.");
                }

                var mappedResult = _mapper.Map<FlightDto[]>(results);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //GET: api/v1.0/flights/i                                 Get flights by id
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightDto>> GetFlightById(long id)
        {
            try
            {
                var result = await _flightRepository.GetFlightByID(id);
                
                if (result == null)
                {
                    return NotFound($"Couldn't find any flight with ID: {id}");
                }

                var mappedResult = _mapper.Map<FlightDto>(result);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //GET: api/v1.0/flights/manufacturer=boeing                                 Get flights by manufacturer
        [HttpGet("manufacturer={manufacturer}")]
        public async Task<ActionResult<FlightDto[]>> GetFlightsByManufacturer(string manufacturer)
        {
            try
            {

                var results = await _repository.GetFlightsByManufacturer(manufacturer);
                
                if (results == null)
                {
                    return NotFound($"Couldn't find destination {manufacturer}.");
                }

                var mappedResult = _mapper.Map<FlightDto[]>(results);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //GET: api/v1.0/flights/model=F-92                                 Get flights by model
        [HttpGet("model/{model}")]
        public async Task<ActionResult<FlightDto[]>> GetFlightsByModel(string model)
        {
            try
            {
                var results = await _flightRepository.GetFlightsByModel(model);
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
        public async Task<ActionResult<FlightDto>> PostFlightByID([FromBody]FlightDto flightDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Flight>(flightDto);

                _flightRepository.Add(mappedEntity);
                if (await _flightRepository.Save())
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
        public async Task<ActionResult<FlightDto>> PutFlightByID(long id, [FromBody]FlightDto flightDto)
        {
            try
            {
                var oldFlight = await _flightRepository.GetFlightByID(id);

                if (oldFlight == null)
                {
                    return NotFound($"Couldn't find any flight with id: {id}");
                }

                var newFlight = _mapper.Map(flightDto, oldFlight);
                _flightRepository.Update(newFlight);
               
                if (await _flightRepository.Save())

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


        //DELETE: api/v1.0/flights/1                                 Delete Flight
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFlightByID(long id)
        {
            try
            {
                var oldFlight = await _flightRepository.GetFlightByID(id);

                if (oldFlight == null)
                {
                    return NotFound($"Couldn't find any flight with id: {id}");
                }

                _flightRepository.Delete(oldFlight);

                if (await _flightRepository.Save())
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