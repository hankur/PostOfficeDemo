using System.Threading.Tasks;
using Core.DAL;
using Core.DAL.Repositories;
using Core.Domain;

namespace Core.BLL.Services
{
    public class ShipmentService : BaseService<Shipment>
    {
        protected new readonly ShipmentRepository ServiceRepository;
        
        public ShipmentService(UnitOfWork uow) : base(uow.Shipments)
        {
            ServiceRepository = uow.Shipments;
        }

        public async Task<Shipment> FindIncluded(string number)
        {
            return await ServiceRepository.FindIncluded(number);
        }

        public async Task Finalize(string number)
        {
            var shipment = await Find(number);
            shipment.Finalized = true;
        }
    }
}