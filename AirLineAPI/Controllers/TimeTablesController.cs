using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirLineAPI.Db_Context;
using AirLineAPI.Services;
using AirLineAPI.Model;
using Microsoft.AspNetCore.Http;
using AirLineAPI.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AirLineAPI.Filters;

namespace AirLineAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TimeTablesController : HateoasControllerBase
    {
        private readonly ITimeTableRepository _repository;
        private readonly IMapper _mapper;

        public TimeTablesController(ITimeTableRepository repository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // /API/v1.0/timetables     Get all timetabels
        [HttpGet(Name = "GetTimeTables")]
        public async Task<ActionResult<TimeTableDto[]>> GetTimeTables(int minMinutes, int maxMinutes, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTables(minMinutes, maxMinutes, includePassengers, includeRoutes);
                var passengerresult = _mapper.Map<TimeTableDto[]>(results).Select(m => HateoasMainLinksTimeTable(m));

                if (results == null)
                {
                    return NotFound($"Could not find any timetables");
                }

                return Ok(passengerresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //    /API/v1.0/timetables/1    Get timetable by id

        [HttpGet("{id}", Name = "GettimetablesId")]
        public async Task<ActionResult<TimeTable>> GetTimeTableByID(long id, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTableByID(id, includePassengers, includeRoutes);
                var passengerresult = _mapper.Map<TimeTableDto>(results);
                if (results == null)
                {
                    return NotFound($"Could not find any timetable with id {id}");
                }
                return Ok(HateoasMainLinksTimeTable(passengerresult));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        // API/v1.0/timetables/startdestination=gothenburg     Get timtables with startdestination gothenburg

        [HttpGet("startDestination={startDestination}")]
        public async Task<ActionResult<TimeTable[]>> GetTimeTableByStartDestination(string startDestination, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTableByStartDestination(startDestination, includePassengers, includeRoutes);
                if (results == null)
                {
                    return NotFound($"Could not find Timetable at start destination");
                }
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        // API/v1.0/timetables/enddestination=gothenburg     Get timtables with enddestination= gothenburg

        [HttpGet("endDestination={endDestination}")]
        public async Task<ActionResult<TimeTable[]>> GetTimeTableByEndDestination(string endDestination, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _repository.GetTimeTableByEndDestination(endDestination, includePassengers, includeRoutes);
                if (results == null)
                {
                    return NotFound($"There is no flight with the enddestination : {endDestination}");
                }
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TimeTableDto>> PostEvent(TimeTableDto timetableDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<TimeTable>(timetableDto);
                _repository.Add(mappedEntity);
                if (await _repository.Save())
                {
                    return Created($"/api/v1.0/Timetables/{mappedEntity.Id}", _mapper.Map<TimeTable>(mappedEntity));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }

        //https:/localhost:44333/api/v1.0/timetables/
        [HttpPut("{timetableId}")]
        public async Task<ActionResult> PutTimeTable(long timeTableId, TimeTableDto timeTableDto)
        {
            try
            {
                var oldTimeTable = await _repository.GetTimeTableByID(timeTableId);
                if (oldTimeTable == null)
                {
                    return NotFound($"Could not find timetable with id {timeTableId}");
                }

                var newTimeTable = _mapper.Map(timeTableDto, oldTimeTable);
                _repository.Update(newTimeTable);
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

        [HttpDelete("{timeTableId}")]
        public async Task<ActionResult> DeleteTimeTable(int timeTableId)
        {
            try
            {
                var oldTimeTable = await _repository.GetTimeTableByID(timeTableId);
                if (oldTimeTable == null)
                {
                    return NotFound($"Counld not find timetable with id {timeTableId}");
                }
                _repository.Delete(oldTimeTable);
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