using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Repositories
{
    public class BaseRepository<TDomainEntity>
        where TDomainEntity : class, IEntity, new()
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

        public virtual async Task<TDomainEntity> Find(string number)
        {
            return await RepositoryDbSet.FindAsync(number);
        }

        public virtual async Task<bool> Exists(string number)
        {
            return await RepositoryDbSet.AnyAsync(b => b.Number == number);
        }

        public virtual async Task<TDomainEntity> Add(TDomainEntity entity)
        {
            return (await RepositoryDbSet.AddAsync(entity)).Entity;
        }

        public virtual void Remove(TDomainEntity entity)
        {
            RepositoryDbSet.Remove(entity);
        }

        public virtual void Remove(string number)
        
        {
            Remove(RepositoryDbSet.Find(number));
        }
    }
}