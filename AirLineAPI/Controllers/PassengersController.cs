using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirLineAPI.Db_Context;
using AirLineAPI.Services;
using AirLineAPI.Dto;
using Microsoft.EntityFrameworkCore;

namespace AirLineAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly AirLineContext _context;

        public PassengersController(AirLineContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<string> Get()
        {
            var passenger = _context.Passengers.Where(p => p.ID == 1).FirstOrDefaultAsync();
            return passenger.Result.Name;
        }

    }
}