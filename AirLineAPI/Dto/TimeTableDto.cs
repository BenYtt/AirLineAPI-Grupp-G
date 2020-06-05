using AirLineAPI.HATEOAS;
using AirLineAPI.Model;
using AirLineAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Dto
{
    public class TimeTableDto : HateoasLinkBase
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public Flight Flight { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public ICollection<Passenger> Passengers { get; set; }
    }
}