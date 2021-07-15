using System.Threading.Tasks;
using Core.DAL;
using Core.DAL.Repositories;
using Core.Domain;

namespace Core.BLL.Services
{
    public class BagService : BaseService<Bag>
    {
        protected new readonly BagRepository ServiceRepository;

        public BagService(UnitOfWork uow) : base(uow.Bags)
        {
            ServiceRepository = uow.Bags;
        }

        public static Bag Add(Bag bag, Shipment shipment)
        {
            shipment.Bags.Add(bag);
            return bag;
        }

        public async Task<Bag> Update(Bag bagModel)
        {
            var bag = await FindIncluded(bagModel.Number);

            bag.LetterCount = bagModel.LetterCount;
            bag.Price = bagModel.Price;
            bag.Weight = bagModel.Weight;
            bag.ShipmentNumber = bagModel.ShipmentNumber;

            return bag;
        }

        public async Task<Bag> FindIncluded(string number)
        {
            return await ServiceRepository.FindIncluded(number);
        }

        public new async Task Remove(string number)
        {
            var bag = await FindIncluded(number);
            base.Remove(bag);
        }
    }
}