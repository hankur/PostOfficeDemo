using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Bag;
using Newtonsoft.Json;

namespace Core.Domain
{
    public class Parcel
    {
        [Key]
        [Required]
        [StringLength(10)]
        [DisplayName("Parcel number")]
        // Matches if string is in this format: "LLNNNNNNLL", where L – letter, N – digit
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{6}[a-zA-Z]{2}$")]
        public string Number { get; set; }
        
        [Required]
        [StringLength(100)]
        [DisplayName("Recipient name")]
        public string Recipient { get; set; }
        
        [Required]
        [StringLength(2)]
        [DisplayName("Destination country")]
        [RegularExpression(@"^[A-Z]{2}$")]
        public string Destination { get; set; }
        
        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Weight { get; set; }

        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Price { get; set; }

        [ForeignKey("ParcelBag")]
        public string BagNumber { get; set; }
        [JsonIgnore]
        public ParcelBag Bag { get; set; }
    }
}