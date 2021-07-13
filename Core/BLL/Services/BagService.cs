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

        public async Task<Bag> FindIncluded(string number)
        {
            return await ServiceRepository.FindIncluded(number);
        }
    }
}