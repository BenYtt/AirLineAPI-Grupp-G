using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirLineAPI.Db_Context;
using AirLineAPI.Repository;

namespace AirLineAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    public class TimeTablesController : ControllerBase
    {
        private ITimeTableRepository repo;
        //private readonly AirLineContext _context;

        public TimeTablesController(ITimeTableRepository repository)
        {
            repo = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeTableById(int id)
        {
            try
            {
                var timeTbale = await repo.GetTimeTableById(id);
                if (timeTbale == null)
                {
                    return NotFound();
                }
                return Ok(timeTbale);

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


    }
}