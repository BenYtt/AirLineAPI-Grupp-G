﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class Route
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Destination StartDestination { get; set; }
        public Destination EndDestination { get; set; }
        public TimeSpan TravelTime { get; set; }
    }
}
