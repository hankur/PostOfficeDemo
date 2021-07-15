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

        public async Task<Shipment> Update(Shipment shipmentModel)
        {
            var shipment = await FindIncluded(shipmentModel.Number);

            shipment.Airport = shipmentModel.Airport;
            shipment.FlightDate = shipmentModel.FlightDate;
            shipment.FlightNumber = shipmentModel.FlightNumber;

            return shipment;
        }

        public async Task<Shipment> FindIncluded(string number)
        {
            return await ServiceRepository.FindIncluded(number);
        }

        public static Shipment Finalize(Shipment shipment)
        {
            shipment.Finalized = true;
            return shipment;
        }

        public new async Task Remove(string number)
        {
            var shipment = await FindIncluded(number);
            base.Remove(shipment);
        }
    }
}