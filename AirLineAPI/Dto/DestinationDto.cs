using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AirLineAPI.HATEOAS;
using AirLineAPI.Services.Interfaces;

namespace AirLineAPI.Dto
{
    public class DestinationDto : HateoasLinkBase, IEntity
    {
        [Required]
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}