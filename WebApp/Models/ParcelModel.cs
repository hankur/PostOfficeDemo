using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ParcelModel
    {
        [Required]
        [DisplayName("Parcel number")]
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{6}[a-zA-Z]{2}$", 
            ErrorMessage = "Value must be in the format 'LLNNNNNNLL', where L – letter, N – digit")]
        public string Number { get; set; }
        
        [Required]
        [StringLength(100)]
        [DisplayName("Recipient name")]
        public string Recipient { get; set; }
        
        [Required]
        [DisplayName("Destination country")]
        [RegularExpression(@"^[A-Z]{2}$", 
            ErrorMessage = "Value must be in the format 'LL', where L – uppercase letter")]
        public string Destination { get; set; }
        
        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Weight { get; set; }

        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Bag number")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,15}$", 
            ErrorMessage = "Value can only contain 1-15 letters and/or digits")]
        public string BagNumber { get; set; }
    }
}