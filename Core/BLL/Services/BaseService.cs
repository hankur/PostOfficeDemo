using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DAL.Repositories;

namespace Core.BLL.Services
{
    public abstract class BaseService<TDomainEntity>
        where TDomainEntity : class, new()
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

        public virtual async Task<TDomainEntity> Find(params object[] id)
        {
            return await ServiceRepository.Find(id);
        }

        public virtual async Task<TDomainEntity> Add(TDomainEntity entity)
        {
            return await ServiceRepository.Add(entity);
        }
    }
}