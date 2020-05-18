using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Dto
{
    public class DestinationDto
    {
        [Required]
        public long ID { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
