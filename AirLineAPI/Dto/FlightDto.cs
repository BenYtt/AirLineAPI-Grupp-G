using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Dto
{
    public class FlightDto
    {
        [Required]
        public long ID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}
