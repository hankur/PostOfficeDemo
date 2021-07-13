using System;
using System.Collections.Generic;
using Core.DAL;

namespace Core.BLL.Helpers
{
    public class ServiceProvider
    {
        protected readonly Dictionary<Type, object> ServiceCache;
        protected readonly ServiceFactory ServiceFactory;
        protected readonly UnitOfWork Uow;

        public ServiceProvider(ServiceFactory serviceFactory, UnitOfWork uow)
            : this(new Dictionary<Type, object>(), serviceFactory, uow)
        {
        }

        public ServiceProvider(Dictionary<Type, object> serviceCache, ServiceFactory serviceFactory,
            UnitOfWork uow)
        {
            ServiceCache = serviceCache;
            ServiceFactory = serviceFactory;
            Uow = uow;
        }

        public virtual TService GetService<TService>()
        {
            if (ServiceCache.ContainsKey(typeof(TService)))
                return (TService) ServiceCache[typeof(TService)];

            var serviceCreationMethod = ServiceFactory.GetServiceFactory<TService>();
            var service = serviceCreationMethod(Uow);

            ServiceCache[typeof(TService)] = service;
            return (TService) service;
        }
    }
}