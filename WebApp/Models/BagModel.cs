using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace WebApp.Models
{
    public class BagModel
    {
        [Required]
        [DisplayName("Bag number")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,15}$", 
            ErrorMessage = "Value can only contain 1-15 letters and/or digits")]
        public string Number { get; set; }
        
        [Required]
        [DisplayName("Bag type")]
        public BagType Type { get; set; }

        [DisplayName("Count of letters")]
        [Range(1, int.MaxValue)]
        public int? LetterCount { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal? Weight { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal? Price { get; set; }
        
        [Required]
        [DisplayName("Shipment number")]
        [RegularExpression(@"^[a-zA-Z0-9]{3}-[a-zA-Z0-9]{6}$", 
            ErrorMessage = "Value must be in the format 'XXX-XXXXXX', where X – letter or digit")]
        public string ShipmentNumber { get; set; }
    }
}