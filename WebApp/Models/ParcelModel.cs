using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ParcelModel
    {
        [Required]
        [StringLength(10)]
        // Matches if string is in this format: "LLNNNNNNLL", where L – letter, N – digit
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{6}[a-zA-Z]{2}$")]
        public string Number { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Recipient { get; set; }
        
        [Required]
        [StringLength(2)]
        [RegularExpression(@"^[A-Z]{2}$")]
        public string Destination { get; set; }
        
        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Weight { get; set; }

        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(15)]
        // Matches if string contains only letters and numbers
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string BagNumber { get; set; }
    }
}