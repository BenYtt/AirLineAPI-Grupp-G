using System;
using AirLineAPI.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AirLineAPI.HATEOAS;
using AirLineAPI.Services.Interfaces;

namespace AirLineAPI.Dto
{
    public class PassengerDto : HateoasLinkBase, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long IdentificationNumber { get; set; }
        public ICollection<PassengerTimeTable> PassengerTimeTables { get; set; }
    }
}