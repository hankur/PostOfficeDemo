using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Core.Domain.Bag
{
    public class Bag
    {
        [Key]
        [Required]
        [StringLength(15)]
        [DisplayName("Bag number")]
        // Matches if string contains only letters and numbers
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Number { get; set; }
        
        [ForeignKey("Shipment")]
        public string ShipmentNumber { get; set; }
        [JsonIgnore]
        public Shipment Shipment { get; set; }
    }
}