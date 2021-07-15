using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Core.Domain
{
    public class Parcel : IEntity
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string Number { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Recipient { get; set; }
        
        [Required]
        [StringLength(2)]
        public string Destination { get; set; }
        
        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Weight { get; set; }

        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Price { get; set; }

        public string BagNumber { get; set; }
        [JsonIgnore]
        public Bag Bag { get; set; }
    }
}