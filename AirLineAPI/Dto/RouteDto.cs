using AirLineAPI.HATEOAS;
using AirLineAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Dto
{
    public class RouteDto : HateoasLinkBase, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DestinationDto StartDestination { get; set; }
        public DestinationDto EndDestination { get; set; }
        public TimeSpan TravelTime { get; set; }
    }
}