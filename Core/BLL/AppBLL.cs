using System.Threading.Tasks;
using Core.BLL.Helpers;
using Core.BLL.Services;
using Core.DAL;

namespace Core.BLL
{
    public class AppBLL
    {
        protected readonly ServiceProvider ServiceProvider;
        protected readonly UnitOfWork UnitOfWork;

        public AppBLL(UnitOfWork unitOfWork, ServiceProvider serviceProvider)
        {
            UnitOfWork = unitOfWork;
            ServiceProvider = serviceProvider;
        }

        public ShipmentService Shipments => ServiceProvider.GetService<ShipmentService>();
        public BagService Bags => ServiceProvider.GetService<BagService>();
        public ParcelService Parcels => ServiceProvider.GetService<ParcelService>();

        public async Task<int> SaveChangesAsync()
        {
            return await UnitOfWork.SaveChangesAsync();
        }
    }
}