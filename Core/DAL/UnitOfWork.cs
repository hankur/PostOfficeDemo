using System.Threading.Tasks;
using Core.DAL.Base;
using Core.DAL.Helpers;
using Core.DAL.Repositories;

namespace Core.DAL
{
    public class UnitOfWork
    {
        protected readonly AppDbContext UowDbContext;
        protected readonly RepositoryProvider RepositoryProvider;
        
        public LetterBagRepository LetterBags =>
            RepositoryProvider.GetRepository<LetterBagRepository>();
        public ParcelBagRepository ParcelBags =>
            RepositoryProvider.GetRepository<ParcelBagRepository>();
        public ParcelRepository Parcels =>
            RepositoryProvider.GetRepository<ParcelRepository>();
        public ShipmentRepository Shipments =>
            RepositoryProvider.GetRepository<ShipmentRepository>();

        public UnitOfWork(AppDbContext dataContext, RepositoryProvider repositoryProvider)
        {
            RepositoryProvider = repositoryProvider;
            UowDbContext = dataContext;
        }

        public BaseRepository<TDomainEntity> BaseRepository<TDomainEntity>()
            where TDomainEntity : class, new()
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