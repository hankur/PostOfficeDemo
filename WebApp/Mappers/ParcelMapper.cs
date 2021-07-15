using Core.Domain;
using WebApp.Models;

namespace WebApp.Mappers
{
    /// <summary>
    ///     Mapper to convert between ParcelModels and Parcels
    /// </summary>
    public static class ParcelMapper
    {
        /// <summary>
        ///     Convert from ParcelModel to Parcel
        /// </summary>
        public static Parcel MapToDomain(ParcelModel model)
        {
            return model == null
                ? null
                : new Parcel
                {
                    Number = model.Number,
                    Recipient = model.Recipient,
                    Destination = model.Destination,
                    Weight = model.Weight,
                    Price = model.Price,
                    BagNumber = model.BagNumber
                };
        }
    }
}