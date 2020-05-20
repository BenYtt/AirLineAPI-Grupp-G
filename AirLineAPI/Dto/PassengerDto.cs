using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirLineAPI.Dto
{
    public class PassengerDto
    {
        [Required]
        public long ID { get; set; }
        [Required]
        public string Name { get; set; }
        public long IdentificationNumber { get; set; }
        public ICollection<PassengerTimeTableDto> PassengerTimeTables { get; set; }

    }
}
