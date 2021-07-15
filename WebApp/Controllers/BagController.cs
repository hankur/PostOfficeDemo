using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.BLL;
using Core.BLL.Services;
using Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Mappers;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    ///     Controller for bag-related endpoints
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BagController : ControllerBase
    {
        /// <inheritdoc />
        public BagController(AppBLL appBLL)
        {
            AppBLL = appBLL;
        }

        private AppBLL AppBLL { get; }

        /// <summary>Get all bags in the system (not including parcels)</summary>
        /// <returns>List of bags</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("List")]
        public async Task<ActionResult<List<Bag>>> GetBags()
        {
            return Ok(await AppBLL.Bags.All());
        }

        /// <summary>Get the bag (without included parcels) by specified number</summary>
        /// <param name="number">Bag number</param>
        /// <returns>Bag</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Number/{number}")]
        public async Task<ActionResult<Bag>> GetBag(string number)
        {
            var bag = await AppBLL.Bags.Find(number);
            if (bag != null)
                return Ok(bag);

            ModelState.AddModelError(nameof(BagModel.Number),
                "Bag with specified number does not exist");
            return BadRequest(ModelState);
        }

        /// <summary>Create a new bag</summary>
        /// <returns>A newly created bag</returns>
        /// <param name="bagModel">Bag model</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Bag>> CreateBag(BagModel bagModel)
        {
            var bag = await ValidateAndCreate(bagModel);
            if (bag == null)
                return BadRequest(ModelState);

            await AppBLL.SaveChangesAsync();
            return Ok(bag);
        }

        /// <summary>Create many new bags</summary>
        /// <returns>A list of newly created bags</returns>
        /// <param name="bagModels">List of bag models</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("List")]
        public async Task<ActionResult<List<Bag>>> CreateBags(List<BagModel> bagModels)
        {
            var newBags = new List<Bag>();
            foreach (var bagModel in bagModels)
            {
                var bag = await ValidateAndCreate(bagModel);
                if (bag == null)
                    return BadRequest(ModelState);

                newBags.Add(bag);
            }

            await AppBLL.SaveChangesAsync();
            return Ok(newBags);
        }

        /// <summary>Update the bag</summary>
        /// <returns>Updated bag</returns>
        /// <param name="bagModel">Bag model</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<Bag>> UpdateBag(BagModel bagModel)
        {
            var bag = await ValidateAndUpdate(bagModel);
            if (bag == null)
                return BadRequest(ModelState);

            await AppBLL.SaveChangesAsync();
            return Ok(bag);
        }

        /// <summary>Delete the bag with parcels</summary>
        /// <param name="number">Bag number</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("Number/{number}")]
        public async Task<ActionResult> Delete(string number)
        {
            var bag = await AppBLL.Bags.FindIncluded(number);
            if (bag == null)
            {
                ModelState.AddModelError(nameof(BagModel.Number), "Bag not found");
                return BadRequest(ModelState);
            }

            var shipment = await AppBLL.Shipments.Find(bag.ShipmentNumber);
            ValidateShipment(shipment);

            if (ModelState.ErrorCount > 0)
                return BadRequest(ModelState);

            AppBLL.Bags.Remove(bag);
            await AppBLL.SaveChangesAsync();
            return NoContent();
        }

        private async Task<Bag> ValidateAndUpdate(BagModel bagModel)
        {
            var shipment = await AppBLL.Shipments.Find(bagModel.ShipmentNumber);

            ValidateBagModel(bagModel);
            ValidateShipment(shipment);

            if (!await AppBLL.Bags.Exists(bagModel.Number))
                ModelState.AddModelError(nameof(BagModel.Number), "Bag not found");

            if (ModelState.ErrorCount > 0)
                return null;

            return await AppBLL.Bags.Update(BagMapper.MapToDomain(bagModel));
        }

        private async Task<Bag> ValidateAndCreate(BagModel bagModel)
        {
            var shipment = await AppBLL.Shipments.Find(bagModel.ShipmentNumber);

            ValidateBagModel(bagModel);
            ValidateShipment(shipment);

            if (await AppBLL.Bags.Exists(bagModel.Number))
                ModelState.AddModelError(nameof(BagModel.Number),
                    "Bag with identical Number already created");

            if (ModelState.ErrorCount > 0)
                return null;

            return BagService.Add(BagMapper.MapToDomain(bagModel), shipment);
        }

        private void ValidateShipment(Shipment shipment)
        {
            if (shipment == null)
                ModelState.AddModelError(nameof(BagModel.ShipmentNumber), "Shipment not found");
            else if (shipment.Finalized)
                ModelState.AddModelError(nameof(BagModel.ShipmentNumber),
                    "Shipment is already finalized");
        }

        private void ValidateBagModel(BagModel bag)
        {
            Func<object, bool> isValid;
            switch (bag.Type)
            {
                case BagType.Letters:
                    isValid = o => o != null;
                    break;
                case BagType.Parcels:
                    isValid = o => o == null;
                    break;
                default:
                    ModelState.AddModelError(nameof(BagModel.Type), "This type does not exist");
                    return;
            }

            var errorMessage = bag.Type == BagType.Letters
                ? $"Value cannot be NULL for BagType.{nameof(BagType.Letters)} (Bag number {bag.Number})"
                : $"Value must be NULL for BagType.{nameof(BagType.Letters)} (Bag number {bag.Number})";

            if (!isValid(bag.LetterCount))
                ModelState.AddModelError(nameof(BagModel.LetterCount), errorMessage);
            if (!isValid(bag.Weight))
                ModelState.AddModelError(nameof(BagModel.Weight), errorMessage);
            if (!isValid(bag.Price))
                ModelState.AddModelError(nameof(BagModel.Price), errorMessage);
        }
    }
}