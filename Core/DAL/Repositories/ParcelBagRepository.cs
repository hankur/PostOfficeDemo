using Core.DAL.Base;
using Core.Domain;

namespace Core.DAL.Repositories
{
    public class ParcelBagRepository : BaseRepository<Shipment>
    {
        public ParcelBagRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }
    }
}