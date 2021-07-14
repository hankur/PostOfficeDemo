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
    public class BagController : ControllerBase
    {
        public BagController(AppBLL appBLL)
        {
            AppBLL = appBLL;
        }

        private AppBLL AppBLL { get; }

        [HttpGet("List")]
        public async Task<ActionResult<List<Bag>>> GetBags()
        {
            return Ok(await AppBLL.Bags.All());
        }

        [HttpPost]
        public async Task<ActionResult<Bag>> CreateBag(BagModel bagModel)
        {
            var bag = await ValidateAndCreate(bagModel);
            if (bag == null)
                return BadRequest(ModelState);

            await AppBLL.SaveChangesAsync();
            return Ok(bag);
        }

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

        private async Task<Bag> ValidateAndCreate(BagModel bagModel)
        {
            ValidateBagModel(bagModel);
            if (ModelState.ErrorCount > 0)
                return null;

            var bag = BagMapper.MapToDomain(bagModel);
            var shipment = await AppBLL.Shipments.FindIncluded(bag.ShipmentNumber);

            await ValidateShipment(shipment, bag);
            if (ModelState.ErrorCount > 0)
                return null;

            shipment.Bags.Add(bag);
            return bag;
        }

        private async Task ValidateShipment(Shipment shipment, Bag bag)
        {
            if (shipment == null)
            {
                ModelState.AddModelError(nameof(BagModel.ShipmentNumber), "Shipment not found");
            }
            else
            {
                if (shipment.Finalized)
                    ModelState.AddModelError(nameof(BagModel.ShipmentNumber), "Shipment is already finalized");
            }

            if (await AppBLL.Bags.Find(bag.Number) != null)
                ModelState.AddModelError(nameof(BagModel.Number),
                    "Bag with identical Number already created");
        }

        private void ValidateBagModel(BagModel bag)
        {
            switch (bag.Type)
            {
                case BagType.Letters:
                    var lettersError =
                        $"Value cannot be NULL for BagType Letters (Bag number {bag.Number})";

                    if (bag.LetterCount == null)
                        ModelState.AddModelError(nameof(BagModel.LetterCount), lettersError);
                    if (bag.Weight == null)
                        ModelState.AddModelError(nameof(BagModel.Weight), lettersError);
                    if (bag.Price == null)
                        ModelState.AddModelError(nameof(BagModel.Price), lettersError);

                    break;
                case BagType.Parcels:
                    var parcelsError =
                        $"Value must be NULL for BagType Parcels (Bag number {bag.Number})";

                    if (bag.LetterCount != null)
                        ModelState.AddModelError(nameof(BagModel.LetterCount), parcelsError);
                    if (bag.Weight != null)
                        ModelState.AddModelError(nameof(BagModel.Weight), parcelsError);
                    if (bag.Price != null)
                        ModelState.AddModelError(nameof(BagModel.Price), parcelsError);

                    break;
                default:
                    ModelState.AddModelError(nameof(BagModel.Type), "This type does not exist");
                    break;
            }
        }
    }
}