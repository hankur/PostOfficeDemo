using Core.Domain;
using WebApp.Models;

namespace WebApp.Mappers
{
    public static class ParcelMapper
    {
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