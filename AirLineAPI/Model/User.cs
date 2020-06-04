using AirLineAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
    }
}