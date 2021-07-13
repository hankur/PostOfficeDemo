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
    }
}