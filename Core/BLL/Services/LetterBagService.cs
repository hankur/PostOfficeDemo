using System.Threading.Tasks;
using Core.DAL;
using Core.Domain;
using Core.Domain.Bag;

namespace Core.BLL.Services
{
    public class LetterBagService : BaseService<LetterBag>
    {
        public LetterBagService(UnitOfWork uow)
        {
            ServiceRepository = uow.BaseRepository<LetterBag>();
        }
    }
}