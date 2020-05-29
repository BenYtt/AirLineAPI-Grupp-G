using AirLineAPI.HATEOAS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace AirLineAPI.Dto
{
    public class RouteDto : HateoasLinkBase
    {
        [Required]
        public long ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DestinationDto StartDestination { get; set; }
        [Required]
        public DestinationDto EndDestination { get; set; }
        [Required]
        public TimeSpan TravelTime { get; set; }
    }
}
