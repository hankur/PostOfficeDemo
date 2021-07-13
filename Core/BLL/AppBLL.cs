using System.Threading.Tasks;
using Core.BLL.Helpers;
using Core.BLL.Services;
using Core.DAL;

namespace Core.BLL
{
    public class AppBLL
    {
        protected readonly UnitOfWork UnitOfWork;
        protected readonly ServiceProvider ServiceProvider;

        public AppBLL(UnitOfWork unitOfWork, ServiceProvider serviceProvider)
        {
            UnitOfWork = unitOfWork;
            ServiceProvider = serviceProvider;
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await UnitOfWork.SaveChangesAsync();
        }

        public ShipmentService Shipments => ServiceProvider.GetService<ShipmentService>();
        public LetterBagService LetterBags => ServiceProvider.GetService<LetterBagService>();
        public ParcelBagService ParcelBags => ServiceProvider.GetService<ParcelBagService>();
        public ParcelService Parcels => ServiceProvider.GetService<ParcelService>();
    }
}