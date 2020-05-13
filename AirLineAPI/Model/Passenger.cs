using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class Passenger
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [StringLength(30, MinimumLength =2, ErrorMessage ="Name must be longer than 2 characters and shorter than 30")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Identification number is required")]
        [Range(0, 999999999999)]
        public long IdentificationNumber { get; set; }
        public ICollection<PassengerTimeTable> PassengerTimeTables { get; set; }
    }

}
