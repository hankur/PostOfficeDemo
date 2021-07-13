using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ShipmentModel
    {
        [Required]
        [DisplayName("Shipment number")]
        [RegularExpression(@"^[a-zA-Z0-9]{3}-[a-zA-Z0-9]{6}$", 
            ErrorMessage = "Value must be in the format 'XXX-XXXXXX', where X – letter or digit")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^(TLL)|(RIX)|(HEL)$", 
            ErrorMessage = "Value must be either 'TLL', 'RIX', or 'HEL'")]
        public string Airport { get; set; }
        
        [Required]
        [DisplayName("Flight number")]
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{4}$", 
            ErrorMessage = "Value must be in the format 'LLNNNN', where L – letter, N – digit")]
        public string FlightNumber { get; set; }

        [Required]
        [DisplayName("Flight date")]
        public DateTime FlightDate { get; set; }
    }
}