using System.Threading.Tasks;
using Core.DAL.Helpers;
using Core.DAL.Repositories;
using Core.Domain;

namespace Core.DAL
{
    public class UnitOfWork
    {
        protected readonly RepositoryProvider RepositoryProvider;
        protected readonly AppDbContext UowDbContext;

        public UnitOfWork(AppDbContext dataContext, RepositoryProvider repositoryProvider)
        {
            RepositoryProvider = repositoryProvider;
            UowDbContext = dataContext;
        }

        public BagRepository Bags =>
            RepositoryProvider.GetRepository<BagRepository>();

        public ParcelRepository Parcels =>
            RepositoryProvider.GetRepository<ParcelRepository>();

        public ShipmentRepository Shipments =>
            RepositoryProvider.GetRepository<ShipmentRepository>();

        public BaseRepository<TDomainEntity> BaseRepository<TDomainEntity>()
            where TDomainEntity : class, IEntity, new()
        {
            return RepositoryProvider.GetEntityRepository<TDomainEntity>();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UowDbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return UowDbContext.SaveChanges();
        }
    }
}