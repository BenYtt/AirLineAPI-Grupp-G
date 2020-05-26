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
    public class PassengersController : ControllerBase
    {
        private readonly IPassengerRepo _passengerRepo;
        private readonly IMapper _mapper;
       
        public PassengersController(IPassengerRepo passengerRepo, IMapper mapper)
        {
            _passengerRepo = passengerRepo;
            _mapper = mapper;
        }
        //api/v1.0/Passengers
        [HttpGet]
        public async Task<ActionResult<Passenger[]>> GetPassenger([FromQuery] bool timeTable)
        {
            try
            {
                var result = await _passengerRepo.GetPassengers(timeTable);

                if (result == null)
                {
                    return NotFound();
                }
               
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failor: {e.Message}");
            }

        }

        //api/v1.0/passengers?timeTable=true             Get passengers with time table
        //api/v1.0/passengers/1                          Get a passenger
        //api/v1.0/passengers/1?timeTable=true           Get a passenger with time table
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassengerById(long id, [FromQuery] bool timeTable)
        {
            try

            {  
                var result = await _passengerRepo.GetPassengerById(id, timeTable);
                
                if (result == null)

                {
                    return NotFound($"There is no passenger with id:{id}");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $" Database failed {e.Message}");
            }
        }

        //api/v1.0/passengers/name=Greta      Get passenger by name
        [HttpGet("name={name}")]
        public async Task<ActionResult<Passenger>> GetPassengerByName(string name)
        {
            try
            { 
                var result = await _passengerRepo.GetPassengerByName(name);
                if (result == null)
                {
                    return NotFound($"There is no passenger with name:{name}");
                }
                return Ok(result);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"database failed {e.Message}");
            }

        }

        //api/v1.0/passengers/identityNm=197110316689      Get passenger by Identification number
        [HttpGet("idnumber={idNumber}")]
        public async Task<ActionResult<Passenger>> GetPassengerById(long idNumber)
        {
            try
            { 
                var result = await _passengerRepo.GetPassengerByIdentificationNumber(idNumber);
                if (result == null)
                {
                    return NotFound($"There is no passenger with Identificationnumber:{idNumber}");
                }
               
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $" Database failed {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PassengerDto>> PostEvent(PassengerDto passengerDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Passenger>(passengerDto);
                _passengerRepo.Add(mappedEntity);
                if (await _passengerRepo.Save())
                {
                    return Created($"/api/v1.0/Destinations/{mappedEntity.ID}", _mapper.Map<Passenger>(mappedEntity));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }

        //PUT: api/v1.0/passengers                                 PUT passenger
        [HttpPut]
        public async Task<ActionResult<PassengerDto>> PutEvent(long id, PassengerDto passengerDto)
        {
            try
            {
                var oldpassenger = await _passengerRepo.GetPassengerById(id);

                if (oldpassenger == null)
                {
                    return NotFound($"Couldn't find any passenger with id: {id}");
                }

                var newPassenger = _mapper.Map(passengerDto, oldpassenger);
                _passengerRepo.Update(newPassenger);
               if (await _passengerRepo.Save())
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