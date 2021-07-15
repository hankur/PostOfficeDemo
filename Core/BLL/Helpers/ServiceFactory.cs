using System;
using System.Collections.Generic;
using Core.BLL.Services;
using Core.DAL;

namespace Core.BLL.Helpers
{
    public class ServiceFactory
    {
        private readonly Dictionary<Type, Func<UnitOfWork, object>> _serviceCreationMethodCache;

        public ServiceFactory() : this(new Dictionary<Type, Func<UnitOfWork, object>>())
        {
        }

        public ServiceFactory(Dictionary<Type, Func<UnitOfWork, object>> serviceCreationMethodCache)
        {
            _serviceCreationMethodCache = serviceCreationMethodCache;

            RegisterServices();
        }

        private void RegisterServices()
        {
            AddToCreationMethods(uow => new ShipmentService(uow));
            AddToCreationMethods(uow => new ParcelService(uow));
            AddToCreationMethods(uow => new BagService(uow));
        }


        public virtual void AddToCreationMethods<TService>(
            Func<UnitOfWork, TService> creationMethod)
            where TService : class
        {
            _serviceCreationMethodCache.Add(typeof(TService), creationMethod);
        }


        public virtual Func<UnitOfWork, object> GetServiceFactory<TService>()
        {
            if (_serviceCreationMethodCache.ContainsKey(typeof(TService)))
                return _serviceCreationMethodCache[typeof(TService)];

            throw new NullReferenceException(
                "No service creation method found for " + typeof(TService).FullName);
        }
    }
}