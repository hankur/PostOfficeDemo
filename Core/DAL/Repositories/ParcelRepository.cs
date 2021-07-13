using Core.DAL.Base;
using Core.Domain;

namespace Core.DAL.Repositories
{
    public class ParcelRepository : BaseRepository<Shipment>
    {
        public ParcelRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }
    }
}