using Core.Domain;
using WebApp.Models;

namespace WebApp.Mappers
{
    public static class BagMapper
    {
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