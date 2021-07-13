﻿using System;
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
            var shipment = await ValidateAndCreate(shipmentModel);
            if (shipment == null)
                return BadRequest(ModelState);

            await AppBLL.SaveChangesAsync();
            return Ok(shipment);
        }

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

        [HttpPost("{shipmentNumber}/Finalize")]
        public async Task<ActionResult<Shipment>> Finalize(string shipmentNumber)
        {
            var shipment = await ValidateAndFinalize(shipmentNumber);
            if (shipment == null)
                return BadRequest(ModelState);
            
            await AppBLL.SaveChangesAsync();
            return Ok(shipment);
        }

        private async Task<Shipment> ValidateAndFinalize(string shipmentNumber)
        {
            var shipment = await AppBLL.Shipments.FindIncluded(shipmentNumber);

            if (shipment == null)
            {
                ModelState.AddModelError(nameof(ShipmentModel.Number), "Shipment not found");
                return null;
            }

            if (shipment.FlightDate < DateTime.Now)
                ModelState.AddModelError(nameof(ShipmentModel.FlightDate), 
                    "Flight date cannot be in the past");

            if (shipment.Bags.Count == 0)
                ModelState.AddModelError(nameof(Shipment.Bags),
                    "Shipment is missing bags");

            foreach (var bag in shipment.Bags)
            {
                if (bag.Type == BagType.Parcels && bag.Parcels.Count == 0)
                    ModelState.AddModelError(nameof(Shipment.Bags),
                        $"Bag number '{bag.Number}' is missing parcels");
            }
            
            if (ModelState.ErrorCount > 0)
                return null;

            shipment.Finalized = true;
            return shipment;
        }

        private async Task<Shipment> ValidateAndCreate(ShipmentModel shipmentModel)
        {
            if (await AppBLL.Shipments.Find(shipmentModel.Number) != null)
                ModelState.AddModelError(nameof(ShipmentModel.Number),
                    "Shipment with identical Number already created");

            if (shipmentModel.FlightDate < DateTime.Now)
                ModelState.AddModelError(nameof(ShipmentModel.FlightDate), 
                    "Flight date cannot be in the past");
            
            if (ModelState.ErrorCount > 0)
                return null;

            var shipment = ShipmentMapper.MapToDomain(shipmentModel);
            return await AppBLL.Shipments.Add(shipment);
        }
    }
}