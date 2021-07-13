using System.Collections.Generic;
using System.Threading.Tasks;
using Core.BLL;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.Mappers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ParcelController : ControllerBase
    {
        private AppBLL AppBLL { get; set; }
        
        public ParcelController(AppBLL appBLL)
        {
            AppBLL = appBLL;
        }

        [HttpPost]
        public async Task<ActionResult<Parcel>> CreateParcel(ParcelModel parcelModel)
        {
            var parcel = ParcelMapper.MapToDomain(parcelModel);
            var newParcel = await AppBLL.Parcels.Add(parcel);
            
            await AppBLL.SaveChangesAsync();
            return Ok(newParcel);
        }

        [HttpPost("List")]
        public async Task<ActionResult<List<Parcel>>> CreateParcels(List<ParcelModel> parcelModels)
        {
            var newParcels = new List<Parcel>();
            foreach (var parcelModel in parcelModels)
            {
                var parcel = ParcelMapper.MapToDomain(parcelModel);
                newParcels.Add(await AppBLL.Parcels.Add(parcel));
            }
            
            await AppBLL.SaveChangesAsync();
            return Ok(newParcels);
        }
    }
}