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
        [StringLength(100, MinimumLength = 3)]
        public string City { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Country { get; set; }
    }
}
