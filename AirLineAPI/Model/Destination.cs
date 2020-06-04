using AirLineAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class Destination : IEntity
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}