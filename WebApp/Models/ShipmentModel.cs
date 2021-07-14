using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    /// <summary>
    /// Input model for ShipmentController endpoints
    /// </summary>
    public class ShipmentModel
    {
        /// <summary>Shipment number must be in the format 'XXX-XXXXXX', where X – letter or digit</summary>
        [Required]
        [DisplayName("Shipment number")]
        [RegularExpression(@"^[a-zA-Z0-9]{3}-[a-zA-Z0-9]{6}$", 
            ErrorMessage = "Value must be in the format 'XXX-XXXXXX', where X – letter or digit")]
        public string Number { get; set; }

        /// <summary>Airport value must be either 'TLL', 'RIX', or 'HEL'</summary>
        [Required]
        [RegularExpression(@"^(TLL)|(RIX)|(HEL)$", 
            ErrorMessage = "Value must be either 'TLL', 'RIX', or 'HEL'")]
        public string Airport { get; set; }
        
        /// <summary>Flight number must be in the format 'LLNNNN', where L – letter, N – digit</summary>
        [Required]
        [DisplayName("Flight number")]
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{4}$", 
            ErrorMessage = "Value must be in the format 'LLNNNN', where L – letter, N – digit")]
        public string FlightNumber { get; set; }

        /// <summary>Flight date must be in the future</summary>
        [Required]
        [DisplayName("Flight date")]
        public DateTime FlightDate { get; set; }
    }
}