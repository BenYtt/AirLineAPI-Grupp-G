using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AirLineAPI.Services.Interfaces;

namespace AirLineAPI.Model
{
    public class Passenger : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public long IdentificationNumber { get; set; }

        public ICollection<PassengerTimeTable> PassengerTimeTables { get; set; }
    }
}