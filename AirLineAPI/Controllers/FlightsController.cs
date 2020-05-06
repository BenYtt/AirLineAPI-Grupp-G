using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AirLineAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly AirLineContext _context;

        public FlightsController(AirLineContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string Get()
        {
            var fakecontext = _context.Fakes.FirstOrDefault().Name;
            return fakecontext;
        }
    }
}