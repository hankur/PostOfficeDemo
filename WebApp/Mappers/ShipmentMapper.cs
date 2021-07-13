using System.Collections.Generic;
using Core.Domain;
using WebApp.Models;

namespace WebApp.Mappers
{
    public static class ShipmentMapper
    {
        public static Shipment MapToDomain(ShipmentModel model)
        {
            return model == null
                ? null
                : new Shipment
                {
                    Number = model.Number,
                    Airport = model.Airport,
                    Bags = new List<Bag>(),
                    Finalized = false,
                    FlightDate = model.FlightDate,
                    FlightNumber = model.FlightNumber
                };
        }
    }
}