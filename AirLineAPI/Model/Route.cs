using AirLineAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class Route : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Destination StartDestination { get; set; }
        public Destination EndDestination { get; set; }
        public TimeSpan TravelTime { get; set; }
    }
}