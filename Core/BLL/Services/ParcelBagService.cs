using System.Threading.Tasks;
using Core.DAL;
using Core.Domain;
using Core.Domain.Bag;

namespace Core.BLL.Services
{
    public class ParcelBagService : BaseService<ParcelBag>
    {
        public ParcelBagService(UnitOfWork uow)
        {
            ServiceRepository = uow.BaseRepository<ParcelBag>();
        }
    }
}