using System;
using System.Collections.Generic;
using Core.DAL.Repositories;
using Core.Domain;

namespace Core.DAL.Helpers
{
    public class RepositoryProvider
    {
        protected readonly AppDbContext DataContext;
        protected readonly Dictionary<Type, object> RepositoryCache;
        protected readonly RepositoryFactory RepositoryFactory;

        public RepositoryProvider(RepositoryFactory repositoryFactory,
            AppDbContext dataContext) :
            this(new Dictionary<Type, object>(), repositoryFactory, dataContext)
        {
        }

        public RepositoryProvider(Dictionary<Type, object> repositoryCache,
            RepositoryFactory repositoryFactory, AppDbContext dataContext)
        {
            RepositoryCache = repositoryCache;
            RepositoryFactory = repositoryFactory;
            DataContext = dataContext;
        }

        public TRepository GetRepository<TRepository>()
        {
            if (RepositoryCache.ContainsKey(typeof(TRepository)))
                return (TRepository) RepositoryCache[typeof(TRepository)];

            var repoCreationMethod = RepositoryFactory.GetRepositoryFactory<TRepository>();
            var repo = repoCreationMethod(DataContext);

            RepositoryCache[typeof(TRepository)] = repo;
            return (TRepository) repo;
        }

        public BaseRepository<TDomainEntity> GetEntityRepository<TDomainEntity>()
            where TDomainEntity : class, IEntity, new()
        {
            if (RepositoryCache.ContainsKey(typeof(BaseRepository<TDomainEntity>)))
                return (BaseRepository<TDomainEntity>) 
                    RepositoryCache[typeof(BaseRepository<TDomainEntity>)];

            var repoCreationMethod = RepositoryFactory.GetEntityRepositoryFactory<TDomainEntity>();
            var repo = repoCreationMethod(DataContext);

            RepositoryCache[typeof(BaseRepository<TDomainEntity>)] = repo;
            return (BaseRepository<TDomainEntity>) repo;
        }
    }
}