using System;
using AirLineAPI.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AirLineAPI.HATEOAS;

namespace AirLineAPI.Dto
{
    public class PassengerDto : HateoasLinkBase
    {
        [Required]
        public long ID { get; set; }
        [Required]
        public string Name { get; set; }
        public long IdentificationNumber { get; set; }
        public ICollection<PassengerTimeTable> PassengerTimeTables { get; set; }

    }
}
