using System.Threading.Tasks;
using Core.DAL;
using Core.Domain;

namespace Core.BLL.Services
{
    public class ShipmentService : BaseService<Shipment>
    {
        public ShipmentService(UnitOfWork uow)
        {
            ServiceRepository = uow.BaseRepository<Shipment>();
        }

        public async Task Finalize(string number)
        {
            var shipment = await Find(number);
            shipment.Finalized = true;
        }
    }
}