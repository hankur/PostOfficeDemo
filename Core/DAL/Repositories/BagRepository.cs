using System.Threading.Tasks;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Repositories
{
    public class BagRepository : BaseRepository<Bag>
    {
        public BagRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }

        public async Task<Bag> FindIncluded(string number)
        {
            return await RepositoryDbSet.Include(b => b.Parcels)
                .FirstOrDefaultAsync(s => s.Number == number);
        }
    }
}