using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Base
{
    public class BaseRepository<TDomainEntity>
        where TDomainEntity : class, new()
    {
        protected readonly AppDbContext RepositoryDbContext;
        protected readonly DbSet<TDomainEntity> RepositoryDbSet;

        public BaseRepository(AppDbContext repositoryDbContext)
        {
            RepositoryDbContext = repositoryDbContext;
            RepositoryDbSet = RepositoryDbContext.Set<TDomainEntity>();
        }

        public virtual async Task<List<TDomainEntity>> All()
        {
            return await RepositoryDbSet.ToListAsync();
        }

        public virtual async Task<TDomainEntity> Find(params object[] id)
        {
            return await RepositoryDbSet.FindAsync(id);
        }

        public virtual async Task<TDomainEntity> Add(TDomainEntity entity)
        {
            return (await RepositoryDbSet.AddAsync(entity)).Entity;
        }
    }
}