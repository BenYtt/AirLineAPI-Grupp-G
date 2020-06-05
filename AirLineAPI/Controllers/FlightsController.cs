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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AirLineAPI.Filters;

namespace AirLineAPI.Controllers
{
    // Vi stänger av den för att du ska kunna köra controllern.
    // Kommer att vara aktiv bara på Destinations.
    //[ApiKeyAuth]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class FlightsController : HateoasControllerBase
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;

        public FlightsController(IFlightRepository flightRepository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        //GET: api/v1.0/flights                                 Get Flights
        [HttpGet(Name = "GetFlights")]
        public async Task<ActionResult<FlightDto[]>> GetFlights()
        {
            try
            {
                var results = await _flightRepository.GetFlights();
                var flightResults = _mapper.Map<FlightDto[]>(results).Select(m => HateoasMainLinksFlight(m));

                if (results == null)
                {
                    return NotFound("Could not find any flights.");
                }
                return Ok(flightResults);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //GET: api/v1.0/flights/{id}                                 Get flights by id
        [HttpGet("{id}", Name = "GetFlightById")]
        public async Task<ActionResult<FlightDto>> GetFlightById(int id)
        {
            try
            {
                var result = await _flightRepository.GetFlightById(id);
                var flightResults = _mapper.Map<FlightDto>(result);
                if (result == null)
                {
                    return NotFound($"Couldn't find any flight with ID: {id}");
                }

                return Ok(HateoasMainLinksFlight(flightResults));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //GET: api/v1.0/flights/manufacturer=boeing                                 Get flights by manufacturer
        [HttpGet("manufacturer={manufacturer}", Name = "GetFlightsByManufacturer")]
        public async Task<ActionResult<FlightDto[]>> GetFlightsByManufacturer(string manufacturer)
        {
            try
            {
                var results = await _flightRepository.GetFlightsByManufacturer(manufacturer);
                var flightManufacturerResults = _mapper.Map<FlightDto[]>(results).Select(m => HateoasMainLinksFlight(m));

                if (results == null)
                {
                    return NotFound($"Couldn't find any flight with manufacturer {manufacturer}.");
                }

                return Ok(flightManufacturerResults);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //GET: api/v1.0/flights/model=F-92                                 Get flights by model
        [HttpGet("model={model}", Name = "GetFlightsByModel")]
        public async Task<ActionResult<FlightDto[]>> GetFlightsByModel(string model)
        {
            try
            {
                var results = await _flightRepository.GetFlightsByModel(model);
                var flightModelResults = _mapper.Map<FlightDto[]>(results).Select(m => HateoasMainLinksFlight(m));

                if (results == null)
                {
                    return NotFound($"Couldn't find any flight with model {model}.");
                }

                return Ok(flightModelResults);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //POST: api/v1.0/flights                                 POST Flight
        [HttpPost]
        public async Task<ActionResult<FlightDto>> PostFlightById([FromBody]FlightDto flightDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Flight>(flightDto);
                _flightRepository.Add(mappedEntity);

                if (await _flightRepository.Save())
                {
                    return Created($"/api/v1.0/Flights/{mappedEntity.Id}", _mapper.Map<Flight>(mappedEntity));
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
        public async Task<ActionResult<FlightDto>> PutFlightById(int id, [FromBody]FlightDto flightDto)
        {
            try
            {
                var oldFlight = await _flightRepository.GetFlightById(id);

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
        public async Task<ActionResult> DeleteFlightById(int id)
        {
            try
            {
                var oldFlight = await _flightRepository.GetFlightById(id);

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