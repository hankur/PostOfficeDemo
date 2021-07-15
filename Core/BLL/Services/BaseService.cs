using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DAL.Repositories;
using Core.Domain;

namespace Core.BLL.Services
{
    public abstract class BaseService<TDomainEntity>
        where TDomainEntity : class, IEntity, new()
    {
        protected BaseRepository<TDomainEntity> ServiceRepository;

        protected BaseService(BaseRepository<TDomainEntity> serviceRepository)
        {
            ServiceRepository = serviceRepository;
        }
        
        public virtual async Task<List<TDomainEntity>> All()
        {
            return await ServiceRepository.All();
        }

        public virtual async Task<TDomainEntity> Find(string number)
        {
            return await ServiceRepository.Find(number);
        }

        public virtual async Task<bool> Exists(string number)
        {
            return await ServiceRepository.Exists(number);
        }

        public virtual async Task<TDomainEntity> Add(TDomainEntity entity)
        {
            return await ServiceRepository.Add(entity);
        }

        public virtual void Remove(string number)
        {
            ServiceRepository.Remove(number);
        }
    }
}