using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

using AutoMapper;

using AirLineAPI.HATEOAS;
using AirLineAPI.Controllers;
using AirLineAPI.Dto;
using AirLineAPI.Model;

namespace BaldBeardedBuilder.HATEOAS.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : HateoasControllerBase
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
            RootModel rootModel = new RootModel();

            rootModel.Links.Add(
                UrlLink("destinations", "GetDestinations", null));
            rootModel.Links.Add(
                UrlLink("Flights", "GetFlights", null));
            rootModel.Links.Add(
                UrlLink("Passengers", "GetPassengers", null));
            rootModel.Links.Add(
                UrlLink("TimeTable", "GetTimeTables", null));
            rootModel.Links.Add(
                UrlLink("RouteTable", "GetRoutes", null));

            return Ok(rootModel);
        }
    }
}