using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Core.Domain
{
    public class Bag : IEntity
    {
        [Key]
        [Required]
        [StringLength(15)]
        public string Number { get; set; }

        [Required]
        public BagType Type { get; set; }
        
        [Range(1, int.MaxValue)]
        public int? LetterCount { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal? Weight { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal? Price { get; set; }

        public List<Parcel> Parcels { get; set; } = new();
        
        public string ShipmentNumber { get; set; }
        [JsonIgnore]
        public Shipment Shipment { get; set; }
    }

    public enum BagType
    {
        Parcels = 0,
        Letters = 1
    }
}