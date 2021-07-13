using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    public class Shipment
    {
        [Key]
        [Required]
        [StringLength(10)]
        [DisplayName("Shipment number")]
        // Matches if string is in this format: "XXX-XXXXXX", where X – letter or digit
        [RegularExpression(@"^[a-zA-Z0-9]{3}-[a-zA-Z0-9]{6}$")]
        public string Number { get; set; }

        [Required]
        public Airport Airport { get; set; }
        
        [Required]
        [StringLength(6)]
        [DisplayName("Flight number")]
        // Matches if string is in this format: "LLNNNN", where L – letter, N – digit
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{4}$")]
        public string FlightNumber { get; set; }

        [Required]
        [DisplayName("Flight date")]
        public DateTime FlightDate { get; set; }

        [NotMapped]
        [DisplayName("List of bags")]
        public ICollection<Bag.Bag> Bags { get; set; }

        [Required]
        public bool Finalized { get; set; }
    }

    public enum Airport
    {
        TLL = 0,
        RIX = 1,
        HEL = 2
    }
}