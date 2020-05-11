using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirLineAPI.Db_Context;
using AirLineAPI.Services;

namespace AirLineAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        //private readonly AirLineContext _context;
        private IDestinationRepository repo;
        public DestinationsController(IDestinationRepository repository)
        {
            repo = repository;
        }
        
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetDestinationById(int id)
        {
            try
            {
                var destination = await repo.GetDestinationById(id);
                if (destination == null)
                {
                    return NotFound();
                }
                return Ok(destination);

            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
        //[HttpGet]
        //public string GetDestinationById()
        //{
        //    return "Hej";

        //}
    }
}