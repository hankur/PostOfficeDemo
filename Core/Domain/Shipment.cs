using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain
{
    public class Shipment
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string Number { get; set; }

        [Required]
        [StringLength(3)]
        public string Airport { get; set; }
        
        [Required]
        [StringLength(6)]
        public string FlightNumber { get; set; }

        [Required]
        public DateTime FlightDate { get; set; }

        public List<Bag> Bags { get; set; } = new();

        [Required]
        public bool Finalized { get; set; }
    }

    public enum Airport
    {
        TLL, RIX, HEL
    }
}