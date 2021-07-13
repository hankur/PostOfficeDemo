using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class BagModel
    {
        [Required]
        [StringLength(15)]
        // Matches if string contains only letters and numbers
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Number { get; set; }
        
        [Required]
        [StringLength(10)]
        // Matches if string is in this format: "XXX-XXXXXX", where X – letter or digit
        [RegularExpression(@"^[a-zA-Z0-9]{3}-[a-zA-Z0-9]{6}$")]
        public string ShipmentNumber { get; set; }
    }
}