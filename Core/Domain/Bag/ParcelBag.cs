using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Domain.Bag
{
    public class ParcelBag : Bag
    {
        [JsonIgnore]
        public List<Parcel> Parcels { get; set; }
    }  
}