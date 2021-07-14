using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    /// <summary>
    /// Input model for ParcelController endpoints
    /// </summary>
    public class ParcelModel
    {
        /// <summary>Shipment number must be in the format 'LLNNNNNNLL', where L – letter, N – digit</summary>
        [Required]
        [DisplayName("Parcel number")]
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{6}[a-zA-Z]{2}$", 
            ErrorMessage = "Value must be in the format 'LLNNNNNNLL', where L – letter, N – digit")]
        public string Number { get; set; }
        
        /// <summary>Recipient name can contain up to 100 characters</summary>
        [Required]
        [StringLength(100)]
        [DisplayName("Recipient name")]
        public string Recipient { get; set; }
        
        /// <summary>Destination country must be in the format 'LL', where L – uppercase letter</summary>
        [Required]
        [DisplayName("Destination country")]
        [RegularExpression(@"^[A-Z]{2}$", 
            ErrorMessage = "Value must be in the format 'LL', where L – uppercase letter")]
        public string Destination { get; set; }
        
        /// <summary>
        /// Weight, NULL for BagType.Parcels, at least 0 for BagType.Letters.
        /// Max 3 places allowed after decimal
        /// </summary>
        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Weight { get; set; }

        /// <summary>
        /// Price, NULL for BagType.Parcels, at least 0 for BagType.Letters.
        /// Max 2 places allowed after decimal
        /// </summary>
        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>Bag number, allowed only 1-15 letters and/or digits</summary>
        [Required]
        [DisplayName("Bag number")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,15}$", 
            ErrorMessage = "Value can only contain 1-15 letters and/or digits")]
        public string BagNumber { get; set; }
    }
}