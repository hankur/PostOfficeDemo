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
    public class ShipmentController : ControllerBase
    {
        public ShipmentController(AppBLL appBLL)
        {
            AppBLL = appBLL;
        }

        private AppBLL AppBLL { get; }


        [HttpGet("List")]
        public async Task<ActionResult<List<Shipment>>> GetShipments()
        {
            return Ok(await AppBLL.Shipments.All());
        }
        
        [HttpPost]
        public async Task<ActionResult<Shipment>> CreateShipment(ShipmentModel shipmentModel)
        {
            var shipment = ShipmentMapper.MapToDomain(shipmentModel);
            var newShipment = await AppBLL.Shipments.Add(shipment);

            await AppBLL.SaveChangesAsync();
            return Ok(newShipment);
        }

        [HttpPost("List")]
        public async Task<ActionResult<List<Shipment>>> CreateShipments(
            List<ShipmentModel> shipmentModels)
        {
            var newShipments = new List<Shipment>();
            foreach (var shipmentModel in shipmentModels)
            {
                var shipment = ShipmentMapper.MapToDomain(shipmentModel);
                newShipments.Add(await AppBLL.Shipments.Add(shipment));
            }

            await AppBLL.SaveChangesAsync();
            return Ok(newShipments);
        }

        [HttpPost("{shipmentNumber}/Finalize")]
        public async Task<ActionResult<Shipment>> Finalize(string shipmentNumber)
        {
            var shipment = await AppBLL.Shipments.Find(shipmentNumber);
            shipment.Finalized = true;

            await AppBLL.SaveChangesAsync();
            return Ok(shipment);
        }
    }
}