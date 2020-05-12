using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class Destination
    {
        public long ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The CityColumn needs to be longer then 3 characters")]
        public string City { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The CountryColumn needs to be longer then 3 characters")]
        public string Country { get; set; }
    }
}
