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

namespace AirLineAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class PassengersController : HateoasControllerBase
    {
        private readonly IPassengerRepo _passengerRepo;
        private readonly IMapper _mapper;
       
        public PassengersController(IPassengerRepo passengerRepo, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _passengerRepo = passengerRepo;
            _mapper = mapper;
        }
        //api/v1.0/Passengers    Get all Passengers
         [HttpGet(Name = "GetAll")]
         public async Task<IActionResult> Get()
        {
            try
            {
               var result = await _passengerRepo.GetPassengers();
               var passengerresult = _mapper.Map<PassengerDto[]>(result).Select(m => HateoasMainLinks(m));
              
                return Ok(passengerresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failor: {e.Message}");
            }

        }

        //api/v1.0/passengers?timeTable=true             Get passengers with time table
        //api/v1.0/passengers/1                          Get a passenger
        //api/v1.0/passengers/1?timeTable=true           Get a passenger with time table
        [HttpGet("{id}", Name = "GetpassengerAsync")]
        public async Task<ActionResult<Passenger>> GetPassengerById(long id, [FromQuery] bool timeTable)
        {
            try

            {  
                var result = await _passengerRepo.GetPassengerById(id, timeTable);
                var mappedResult = _mapper.Map<PassengerDto>(result);
                if (result == null)

                {
                    return NotFound($"There is no passenger with id:{id}");
                }

                return Ok(HateoasMainLinks(mappedResult));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $" Database failed {e.Message}");
            }
        }

        //api/v1.0/passengers/name=Greta      Get passenger by name
        [HttpGet("name = {name}")]
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

        //https:/localhost:44333/api/v1.0/passengers/
        [HttpPut("{Id}")]
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
        //Delete: api/v1.0/Passenger/<id>                                 Delete Passenger
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePassenger(long passengerid)
        {
            try
            {
                var oldpassenger = await _passengerRepo.GetPassengerById(passengerid);
                if (oldpassenger == null)
                {
                    return NotFound($"there is no pasenger with id:{passengerid}");
                }


                _passengerRepo.Delete(oldpassenger);


                if (await _passengerRepo.Save())
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