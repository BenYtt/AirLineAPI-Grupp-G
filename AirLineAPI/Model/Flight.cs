using AirLineAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class Flight
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}