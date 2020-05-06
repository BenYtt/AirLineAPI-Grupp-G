using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AirLineAPI.Controllers
{
    public class DestinationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}