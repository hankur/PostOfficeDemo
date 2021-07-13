using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Repositories
{
    public class ShipmentRepository : BaseRepository<Shipment>
    {
        public ShipmentRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }

        public override async Task<List<Shipment>> All()
        {
            return await RepositoryDbSet.Include(s => s.Bags)
                .ThenInclude(b => b.Parcels).ToListAsync();
        }

        public async Task<Shipment> FindIncluded(string number)
        {
            return await RepositoryDbSet.Include(s => s.Bags)
                .ThenInclude(b => b.Parcels).FirstOrDefaultAsync(s => s.Number == number);
        }
    }
}