using System.Collections.Generic;
using Core.Domain;
using WebApp.Models;

namespace WebApp.Mappers
{
    /// <summary>
    /// Mapper to convert between ShipmentModels and Shipments
    /// </summary>
    public static class ShipmentMapper
    {
        /// <summary>
        /// Convert from ShipmentModel to Shipment
        /// </summary>
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