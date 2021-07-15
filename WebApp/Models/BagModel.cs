using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace WebApp.Models
{
    /// <summary>
    ///     Input model for BagController endpoints
    /// </summary>
    public class BagModel
    {
        /// <summary>Bag number, allowed only 1-15 letters and/or digits</summary>
        [Required]
        [DisplayName("Bag number")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,15}$",
            ErrorMessage = "Value can only contain 1-15 letters and/or digits")]
        public string Number { get; set; }

        /// <summary>Bag type: 0 - Parcels, 1 - Letters</summary>
        [Required]
        [DisplayName("Bag type")]
        public BagType Type { get; set; }

        /// <summary>Count of letters, NULL for BagType.Parcels, at least 1 for BagType.Letters</summary>
        [DisplayName("Count of letters")]
        [Range(1, int.MaxValue)]
        public int? LetterCount { get; set; }

        /// <summary>
        ///     Weight, NULL for BagType.Parcels, at least 0 for BagType.Letters.
        ///     Max 3 places allowed after decimal
        /// </summary>
        [Range(0, (double) decimal.MaxValue)]
        public decimal? Weight { get; set; }

        /// <summary>
        ///     Price, NULL for BagType.Parcels, at least 0 for BagType.Letters.
        ///     Max 2 places allowed after decimal
        /// </summary>
        [Range(0, (double) decimal.MaxValue)]
        public decimal? Price { get; set; }

        /// <summary>Shipment number must be in the format 'XXX-XXXXXX', where X – letter or digit</summary>
        [Required]
        [DisplayName("Shipment number")]
        [RegularExpression(@"^[a-zA-Z0-9]{3}-[a-zA-Z0-9]{6}$",
            ErrorMessage = "Value must be in the format 'XXX-XXXXXX', where X – letter or digit")]
        public string ShipmentNumber { get; set; }
    }
}