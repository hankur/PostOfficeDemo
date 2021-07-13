using System;
using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace WebApp.Models
{
    public class ShipmentModel
    {
        [Required]
        [StringLength(10)]
        // Matches if string is in this format: "XXX-XXXXXX", where X – letter or digit
        [RegularExpression(@"^[a-zA-Z0-9]{3}-[a-zA-Z0-9]{6}$")]
        public string Number { get; set; }

        [Required]
        public Airport Airport { get; set; }
        
        [Required]
        [StringLength(6)]
        // Matches if string is in this format: "LLNNNN", where L – letter, N – digit
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{4}$")]
        public string FlightNumber { get; set; }

        [Required]
        public DateTime FlightDate { get; set; }
    }
}