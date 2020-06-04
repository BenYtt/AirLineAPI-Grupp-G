using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AirLineAPI.HATEOAS;

namespace AirLineAPI.Dto
{
    public class DestinationDto : HateoasLinkBase
    {
        [Required]
        public long Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
