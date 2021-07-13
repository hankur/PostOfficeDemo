using Core.DAL.Base;
using Core.Domain;

namespace Core.DAL.Repositories
{
    public class ShipmentRepository : BaseRepository<Shipment>
    {
        public ShipmentRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }
    }
}