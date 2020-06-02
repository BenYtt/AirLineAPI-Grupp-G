﻿using AirLineAPI.HATEOAS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Dto
{
    public class FlightDto : HateoasLinkBase
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Model { get; set; }
    }
}