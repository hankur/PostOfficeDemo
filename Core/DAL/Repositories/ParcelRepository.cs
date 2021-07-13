using Core.Domain;

namespace Core.DAL.Repositories
{
    public class ParcelRepository : BaseRepository<Parcel>
    {
        public ParcelRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }
    }
}