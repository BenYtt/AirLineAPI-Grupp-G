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
    // Vi stänger av den för att du ska kunna köra controllern.
    // Kommer att vara aktiv bara på Destinations.
    //[ApiKeyAuth]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TimeTablesController : HateoasControllerBase
    {
        private readonly ITimeTableRepository _timeTableRepository;
        private readonly IMapper _mapper;

        public TimeTablesController(ITimeTableRepository timeTableRepository, IMapper mapper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) : base(actionDescriptorCollectionProvider)
        {
            _timeTableRepository = timeTableRepository;
            _mapper = mapper;
        }

        // /API/v1.0/timetables     Get all timetabels
        [HttpGet(Name = "GetTimeTables")]
        public async Task<ActionResult<TimeTableDto[]>> GetTimeTables(int minMinutes, int maxMinutes, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _timeTableRepository.GetTimeTables(minMinutes, maxMinutes, includePassengers, includeRoutes);
                var Timetableresult = _mapper.Map<TimeTableDto[]>(results).Select(m => HateoasMainLinksTimeTable(m));

                if (results == null)
                {
                    return NotFound($"Could not find any timetables");
                }
                return Ok(Timetableresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        //    /API/v1.0/timetables/1    Get timetable by id

        [HttpGet("{id}", Name = "GetTimeTablesId")]
        public async Task<ActionResult<TimeTableDto>> GetTimeTableById(int id, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _timeTableRepository.GetTimeTableById(id, includePassengers, includeRoutes);
                var Timetableresult = _mapper.Map<TimeTableDto>(results);
                if (results == null)
                {
                    return NotFound($"Could not find any timetable with id {id}");
                }
                return Ok(HateoasMainLinksTimeTable(Timetableresult));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        // /api/v1.0/timetables/startdestination%3dgothenburg?includepassengers=true     Get timtables with startdestination gothenburg

        [HttpGet("startDestination={startDestination}", Name = "GetTimeTableByStartDestination")]
        public async Task<ActionResult<TimeTableDto[]>> GetTimeTableByStartDestination(string startDestination, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _timeTableRepository.GetTimeTableByStartDestination(startDestination, includePassengers, includeRoutes);
                var Timetableresult = _mapper.Map<TimeTableDto[]>(results).Select(m => HateoasMainLinksTimeTable(m));
                if (results == null)
                {
                    return NotFound($"Could not find Timetable at start destination");
                }
                return Ok(Timetableresult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        // /api/v1.0/timetables/enddestination%3dgothenburg?includepassengers=true&includeroutes=true    Get timtables with enddestination= gothenburg

        [HttpGet("endDestination={endDestination}", Name = "GetTimeTableByEndDestination")]
        public async Task<ActionResult<TimeTableDto[]>> GetTimeTableByEndDestination(string endDestination, bool includePassengers = false, bool includeRoutes = false)
        {
            try
            {
                var results = await _timeTableRepository.GetTimeTableByEndDestination(endDestination, includePassengers, includeRoutes);
                var Timetableresult = _mapper.Map<TimeTableDto[]>(results).Select(m => HateoasMainLinksTimeTable(m));
                if (results == null)
                {
                    return NotFound($"There is no flight with the enddestination : {endDestination}");
                }
                return Ok(Timetableresult);
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
                _timeTableRepository.Add(mappedEntity);
                if (await _timeTableRepository.Save())
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

        //https:/localhost:44333/api/v1.0/timetables/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<TimeTableDto>> PutTimeTable(int id, TimeTableDto timeTableDto)
        {
            try
            {
                var oldTimeTable = await _timeTableRepository.GetTimeTableById(id);
                if (oldTimeTable == null)
                {
                    return NotFound($"Could not find timetable with id {id}");
                }

                var newTimeTable = _mapper.Map(timeTableDto, oldTimeTable);
                _timeTableRepository.Update(newTimeTable);
                if (await _timeTableRepository.Save())
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
        public async Task<ActionResult> DeleteTimeTable(int id)
        {
            try
            {
                var oldTimeTable = await _timeTableRepository.GetTimeTableById(id);
                if (oldTimeTable == null)
                {
                    return NotFound($"Counld not find timetable with id {id}");
                }
                _timeTableRepository.Delete(oldTimeTable);
                if (await _timeTableRepository.Save())
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