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
    public class RoutesController : ControllerBase
    {
        private readonly AirLineContext _context;
        private IRouteRepository repo;
        public RoutesController(AirLineContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetRouteById(int id)
        {
            try
            {
                var route = await repo.GetRouteById(id);
                if (route == null)
                {
                    return NotFound();
                }
                return Ok(route);

            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
    }
}