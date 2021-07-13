using System.Collections.Generic;
using Core.Domain;
using Core.Domain.Bag;
using WebApp.Models;

namespace WebApp.Mappers
{
    public static class ParcelBagMapper
    {
        public static ParcelBag MapToDomain(ParcelBagModel model)
        {
            return model == null
                ? null
                : new ParcelBag
                {
                    Number = model.Number,
                    ShipmentNumber = model.ShipmentNumber
                };
        }
    }
}