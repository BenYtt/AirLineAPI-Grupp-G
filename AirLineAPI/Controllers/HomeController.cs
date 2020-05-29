using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

using AutoMapper;

using AirLineAPI.Model;
using AirLineAPI.Controllers;
using AirLineAPI.Dto;

namespace BaldBeardedBuilder.HATEOAS.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : HateoasPassengerControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IMapper mapper)
            : base(actionDescriptorCollectionProvider, mapper)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot()
        {
            PassengerDto passengerDto = new PassengerDto();
           
            passengerDto.Links.Add(
                UrlLink("passenger", "GetAll", null));
            
            return Ok(passengerDto);
        }
    }
}
