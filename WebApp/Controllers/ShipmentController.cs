using System;
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
    /// Controller for shipment-related endpoints
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShipmentController : ControllerBase
    {
        /// <inheritdoc />
        public ShipmentController(AppBLL appBLL)
        {
            AppBLL = appBLL;
        }

        private AppBLL AppBLL { get; }
        
        /// <summary>Get the shipment (without included bags) by specified number</summary>
        /// <param name="number">Shipment number</param>
        /// <returns>Shipment</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Number/{number}")]
        public async Task<ActionResult<Shipment>> GetShipment(string number)
        {
            var shipment = await AppBLL.Shipments.Find(number);
            if (shipment != null)
                return Ok(shipment);
            
            ModelState.AddModelError(nameof(ShipmentModel.Number), 
                "Shipment with specified number does not exist");
            return BadRequest(ModelState);
        }

        /// <summary>Get all shipments in the system with included bags and parcels</summary>
        /// <returns>List of shipments</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("List")]
        public async Task<ActionResult<List<Shipment>>> GetShipments()
        {
            return Ok(await AppBLL.Shipments.All());
        }
        
        /// <summary>Create a new shipment</summary>
        /// <returns>A newly created shipment</returns>
        /// <param name="shipmentModel">Shipment model</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Shipment>> CreateShipment(ShipmentModel shipmentModel)
        {
            var shipment = await ValidateAndCreate(shipmentModel);
            if (shipment == null)
                return BadRequest(ModelState);

            await AppBLL.SaveChangesAsync();
            return Ok(shipment);
        }

        /// <summary>Create many new shipments</summary>
        /// <returns>A list of newly created shipments</returns>
        /// <param name="shipmentModels">List of shipment models</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("List")]
        public async Task<ActionResult<List<Shipment>>> CreateShipments(
            List<ShipmentModel> shipmentModels)
        {
            var newShipments = new List<Shipment>();
            foreach (var shipmentModel in shipmentModels)
            {
                var shipment = await ValidateAndCreate(shipmentModel);
                if (shipment == null)
                    return BadRequest(ModelState);

                newShipments.Add(shipment);
            }

            await AppBLL.SaveChangesAsync();
            return Ok(newShipments);
        }

        /// <summary>Update the shipment</summary>
        /// <returns>Updated shipment</returns>
        /// <param name="shipmentModel">Shipment model</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<Shipment>> UpdateShipment(ShipmentModel shipmentModel)
        {
            var shipment = await ValidateAndUpdate(shipmentModel);
            if (shipment == null)
                return BadRequest(ModelState);

            await AppBLL.SaveChangesAsync();
            return Ok(shipment);
        }

        /// <summary>Finalize the shipment so it can't be modified anymore</summary>
        /// <param name="number">Shipment number</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Number/{number}/Finalize")]
        public async Task<ActionResult<Shipment>> Finalize(string number)
        {
            var shipment = await ValidateAndFinalize(number);
            if (shipment == null)
                return BadRequest(ModelState);
            
            await AppBLL.SaveChangesAsync();
            return Ok(shipment);
        }
        
        private async Task<Shipment> ValidateAndCreate(ShipmentModel shipmentModel)
        {
            var shipment = ShipmentMapper.MapToDomain(shipmentModel);
            
            ValidateShipment(shipment);
            
            if (await AppBLL.Shipments.Exists(shipment.Number))
                ModelState.AddModelError(nameof(ShipmentModel.Number),
                    "Shipment with identical Number already created");

            if (ModelState.ErrorCount > 0)
                return null;

            return await AppBLL.Shipments.Add(shipment);
        }
        
        private async Task<Shipment> ValidateAndUpdate(ShipmentModel shipmentModel)
        {
            var shipment = ShipmentMapper.MapToDomain(shipmentModel);
            
            ValidateShipment(shipment);
            
            if (!await AppBLL.Shipments.Exists(shipment.Number))
                ModelState.AddModelError(nameof(ShipmentModel.Number), "Shipment not found");

            if (ModelState.ErrorCount > 0)
                return null;

            return await AppBLL.Shipments.Update(shipment);
        }

        private async Task<Shipment> ValidateAndFinalize(string shipmentNumber)
        {
            var shipment = await AppBLL.Shipments.FindIncluded(shipmentNumber);
            if (shipment == null)
            {
                ModelState.AddModelError(nameof(ShipmentModel.Number), "Shipment not found");
                return null;
            }

            ValidateShipment(shipment);

            if (shipment.Bags.Count == 0)
                ModelState.AddModelError(nameof(Shipment.Bags), "Shipment is missing bags");

            foreach (var bag in shipment.Bags)
            {
                if (bag.Type == BagType.Parcels && bag.Parcels.Count == 0)
                    ModelState.AddModelError(nameof(Shipment.Bags),
                        $"Bag numbered '{bag.Number}' is missing parcels");
            }
            
            if (ModelState.ErrorCount > 0)
                return null;

            shipment.Finalized = true;
            return shipment;
        }

        private void ValidateShipment(Shipment shipment)
        {
            if (shipment.FlightDate < DateTime.Now)
                ModelState.AddModelError(nameof(ShipmentModel.FlightDate), 
                    "Flight date cannot be in the past");
        }
    }
}