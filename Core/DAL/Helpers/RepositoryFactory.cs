using System;
using System.Collections.Generic;
using Core.DAL.Repositories;

namespace Core.DAL.Helpers
{
    public class RepositoryFactory
    {
        private readonly Dictionary<Type, Func<AppDbContext, object>> _repositoryCreationMethodCache;

        public RepositoryFactory() : this(new Dictionary<Type, Func<AppDbContext, object>>())
        {
        }

        public RepositoryFactory(Dictionary<Type, Func<AppDbContext, object>> repositoryCreationMethods)
        {
            _repositoryCreationMethodCache = repositoryCreationMethods;
            
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            AddToCreationMethods(dataContext => new BagRepository(dataContext));
            AddToCreationMethods(dataContext => new ParcelRepository(dataContext));
            AddToCreationMethods(dataContext => new ShipmentRepository(dataContext));
        }

        public void AddToCreationMethods<TRepository>(Func<AppDbContext, TRepository> creationMethod)
            where TRepository : class
        {
            _repositoryCreationMethodCache.Add(typeof(TRepository), creationMethod);
        }


        public Func<AppDbContext, object> GetRepositoryFactory<TRepository>()
        {
            if (_repositoryCreationMethodCache.ContainsKey(typeof(TRepository)))
            {
                return _repositoryCreationMethodCache[typeof(TRepository)];
            }

            throw new NullReferenceException("No repo creation method found for " 
                                             + typeof(TRepository).FullName);
        }

        public Func<AppDbContext, object> GetEntityRepositoryFactory<TDomainEntity>()
            where TDomainEntity : class, new()
        {
            return dataContext => new BaseRepository<TDomainEntity>(dataContext);
        }
    }

}