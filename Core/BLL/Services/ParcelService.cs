using System.Threading.Tasks;
using Core.DAL;
using Core.Domain;
using Core.Domain.Bag;

namespace Core.BLL.Services
{
    public class ParcelService : BaseService<Parcel>
    {
        public ParcelService(UnitOfWork uow)
        {
            ServiceRepository = uow.BaseRepository<Parcel>();
        }
    }
}