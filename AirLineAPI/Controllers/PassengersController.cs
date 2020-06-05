using AutoMapper;
using AirLineAPI.Dto;
using AirLineAPI.Model;
using AirLineAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AirLineAPI.Controllers;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Castle.Core.Internal;
using AirLineAPI.Filters;

namespace AirLineAPI.Controllers
{
    //[ApiKeyAuth]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class PassengersController : HateoasControllerBase
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IMapper _mapper;

        public PassengersController(IPassengerRepository passengerRepo, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _passengerRepository = passengerRepo;
            _mapper = mapper;
        }

        //api/v1.0/Passengers    Get all Passengers
        [HttpGet(Name = "GetPassengers")]
        public async Task<ActionResult<PassengerDto[]>> GetAllPassengers(bool timeTable)
        {
            try
            {
                var result = await _passengerRepository.GetPassengers(timeTable);
                var passengerresult = _mapper.Map<PassengerDto[]>(result).Select(m => HateoasMainLinksPassenger(m));
                if (result == null)
                {
                    return NotFound("Could not find any passengers.");
                }

                return Ok(passengerresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failor: {e.Message}");
            }
        }

        //api/v1.0/passengers/{id}                          Get a passenger
        //api/v1.0/passengers?timeTable=true             Get passengers with time table
        //api/v1.0/passengers/1?timeTable=true           Get a passenger with time table
        [HttpGet("{id}", Name = "GetPassengerById")]
        public async Task<ActionResult<PassengerDto>> GetPassengerById(int id, bool timeTable)
        {
            try

            {
                var result = await _passengerRepository.GetPassengerById(id, timeTable);
                var mappedResult = _mapper.Map<PassengerDto>(result);
                if (result == null)

                {
                    return NotFound($"There is no passenger with ID:{id}");
                }

                return Ok(HateoasMainLinksPassenger(mappedResult));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $" Database failed {e.Message}");
            }
        }

        //api/v1.0/passengers/name=Greta      Get passenger by name
        [HttpGet("name={name}", Name = "GetPassengerByName")]
        public async Task<ActionResult<PassengerDto>> GetPassengerByName(string name)
        {
            try
            {
                var result = await _passengerRepository.GetPassengerByName(name);
                var mappedResult = _mapper.Map<PassengerDto>(result);
                if (result == null)
                {
                    return NotFound($"There is no passenger with name:{name}");
                }
                return Ok(HateoasMainLinksPassenger(mappedResult));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"database failed {e.Message}");
            }
        }

        //api/v1.0/passengers/identityNm=197110316689      Get passenger by Identification number
        [HttpGet("IdentificationNumber={IdentificationNumber}", Name = "GetPassengerByIdentificationNumber")]
        public async Task<ActionResult<PassengerDto>> GetPassengerByIdentificationNumber(long IdentificationNumber)
        {
            try
            {
                var result = await _passengerRepository.GetPassengerByIdentificationNumber(IdentificationNumber);
                var mappedResult = _mapper.Map<PassengerDto>(result);
                if (result == null)
                {
                    return NotFound($"There is no passenger with Identificationnumber:{IdentificationNumber}");
                }

                return Ok(HateoasMainLinksPassenger(mappedResult));
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
                _passengerRepository.Add(mappedEntity);
                if (await _passengerRepository.Save())
                {
                    return Created($"/api/v1.0/Passengers/{mappedEntity.Id}", _mapper.Map<Passenger>(mappedEntity));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }

        //https:/localhost:44333/api/v1.0/passengers/
        [HttpPut("{Id}")]
        public async Task<ActionResult<PassengerDto>> PutEvent(int id, PassengerDto passengerDto)
        {
            try
            {
                var oldpassenger = await _passengerRepository.GetPassengerById(id);

                if (oldpassenger == null)
                {
                    return NotFound($"Couldn't find any passenger with id: {id}");
                }

                var newPassenger = _mapper.Map(passengerDto, oldpassenger);
                _passengerRepository.Update(newPassenger);
                if (await _passengerRepository.Save())
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

        //Delete: api/v1.0/Passenger/{id}                                 Delete Passenger
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePassenger(int id)
        {
            try
            {
                var oldpassenger = await _passengerRepository.GetPassengerById(id);
                if (oldpassenger == null)
                {
                    return NotFound($"there is no pasenger with id:{id}");
                }

                _passengerRepository.Delete(oldpassenger);

                if (await _passengerRepository.Save())
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