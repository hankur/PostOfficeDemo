using Core.DAL.Base;
using Core.Domain;

namespace Core.DAL.Repositories
{
    public class LetterBagRepository : BaseRepository<Shipment>
    {
        public LetterBagRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }
    }
}