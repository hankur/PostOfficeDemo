using Core.Domain;
using WebApp.Models;

namespace WebApp.Mappers
{
    /// <summary>
    /// Mapper to convert between BagModels and Bags
    /// </summary>
    public static class BagMapper
    {
        /// <summary>
        /// Convert from BagModel to Bag
        /// </summary>
        public static Bag MapToDomain(BagModel model)
        {
            return model == null
                ? null
                : new Bag
                {
                    Number = model.Number,
                    ShipmentNumber = model.ShipmentNumber,
                    Type = model.Type,
                    LetterCount = model.LetterCount,
                    Weight = model.Weight,
                    Price = model.Price
                };
        }
    }
}