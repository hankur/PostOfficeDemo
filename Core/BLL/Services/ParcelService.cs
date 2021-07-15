using System.Threading.Tasks;
using Core.DAL;
using Core.Domain;

namespace Core.BLL.Services
{
    public class ParcelService : BaseService<Parcel>
    {
        public ParcelService(UnitOfWork uow) : base(uow.Parcels)
        {
            ServiceRepository = uow.Parcels;
        }

        public Parcel Add(Parcel parcel, Bag bag)
        {
            bag.Parcels.Add(parcel);
            return parcel;
        }

        public async Task<Parcel> Update(Parcel parcelModel)
        {
            var parcel = await Find(parcelModel.Number);
            
            parcel.BagNumber = parcelModel.BagNumber;
            parcel.Destination = parcelModel.Destination;
            parcel.Recipient = parcelModel.Recipient;
            parcel.Weight = parcelModel.Weight;
            parcel.Price = parcelModel.Price;

            return parcel;
        }
    }
}