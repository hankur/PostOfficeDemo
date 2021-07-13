using System.Collections.Generic;
using Core.Domain;
using Core.Domain.Bag;
using WebApp.Models;

namespace WebApp.Mappers
{
    public static class LetterBagMapper
    {
        public static LetterBag MapToDomain(LetterBagModel model)
        {
            return model == null
                ? null
                : new LetterBag
                {
                    Number = model.Number,
                    ShipmentNumber = model.ShipmentNumber,
                    LetterCount = model.LetterCount,
                    Weight = model.Weight,
                    Price = model.Price
                };
        }
    }
}