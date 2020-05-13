using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class Flight
    {
        public long ID { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Value must be between 2 and 30.")]
        public string Manufacturer { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Value must be between 1 and 30.")]
        public string Model { get; set; }
    }
}
