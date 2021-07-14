using System.Collections.Generic;
using System.Threading.Tasks;
using Core.BLL;
using Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Mappers;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for parcel-related endpoints
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ParcelController : ControllerBase
    {
        /// <inheritdoc />
        public ParcelController(AppBLL appBLL)
        {
            AppBLL = appBLL;
        }

        private AppBLL AppBLL { get; }

        /// <summary>Create a new parcel</summary>
        /// <returns>A newly created parcel</returns>
        /// <param name="parcelModel">Parcel model</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Parcel>> CreateParcel(ParcelModel parcelModel)
        {
            var parcel = await ValidateAndCreate(parcelModel);
            if (parcel == null)
                return BadRequest(ModelState);

            await AppBLL.SaveChangesAsync();
            return Ok(parcel);
        }

        /// <summary>Create many new parcels</summary>
        /// <returns>A list of newly created parcels</returns>
        /// <param name="parcelModels">List of parcel models</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("List")]
        public async Task<ActionResult<List<Parcel>>> CreateParcels(List<ParcelModel> parcelModels)
        {
            var newParcels = new List<Parcel>();
            foreach (var parcelModel in parcelModels)
            {
                var parcel = await ValidateAndCreate(parcelModel);
                if (parcel == null)
                    return BadRequest(ModelState);

                newParcels.Add(parcel);
            }

            await AppBLL.SaveChangesAsync();
            return Ok(newParcels);
        }

        private async Task<Parcel> ValidateAndCreate(ParcelModel parcelModel)
        {
            if (decimal.Round(parcelModel.Weight, 3) != parcelModel.Weight)
                ModelState.AddModelError(nameof(ParcelModel.Weight), "Too many decimal places");
            if (decimal.Round(parcelModel.Price, 3) != parcelModel.Price)
                ModelState.AddModelError(nameof(ParcelModel.Price), "Too many decimal places");

            var parcel = ParcelMapper.MapToDomain(parcelModel);
            var bag = await AppBLL.Bags.FindIncluded(parcel.BagNumber);

            if (bag == null)
            {
                ModelState.AddModelError(nameof(ParcelModel.BagNumber), "Bag not found");
            }
            else
            {
                var shipment = await AppBLL.Shipments.Find(bag.ShipmentNumber);
                if (shipment.Finalized)
                    ModelState.AddModelError(nameof(ParcelModel.BagNumber), "Shipment is already finalized");
            }

            if (await AppBLL.Parcels.Find(parcel.Number) != null)
                ModelState.AddModelError(nameof(ParcelModel.Number),
                    "Parcel with identical Number already created");

            if (bag == null || ModelState.ErrorCount > 0)
                return null;

            bag.Parcels.Add(parcel);
            return parcel;
        }
    }
}