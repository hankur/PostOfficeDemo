using System.Collections.Generic;
using System.Threading.Tasks;
using Core.BLL;
using Core.Domain.Bag;
using Microsoft.AspNetCore.Mvc;
using WebApp.Mappers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BagController : ControllerBase
    {
        private AppBLL AppBLL { get; set; }
        
        public BagController(AppBLL appBLL)
        {
            AppBLL = appBLL;
        }

        [HttpPost("Letters")]
        public async Task<ActionResult<LetterBag>> CreateLetterBag(LetterBagModel bagModel)
        {
            var bag = LetterBagMapper.MapToDomain(bagModel);
            var newBag = await AppBLL.LetterBags.Add(bag);
            
            await AppBLL.SaveChangesAsync();
            return Ok(newBag);
        }

        [HttpPost("Letters/List")]
        public async Task<ActionResult<List<LetterBag>>> CreateLetterBags(
            List<LetterBagModel> letterBagModels)
        {
            var newLetterBags = new List<LetterBag>();
            foreach (var letterBagModel in letterBagModels)
            {
                var letterBag = LetterBagMapper.MapToDomain(letterBagModel);
                newLetterBags.Add(await AppBLL.LetterBags.Add(letterBag));
            }
            
            await AppBLL.SaveChangesAsync();
            return Ok(newLetterBags);
        }

        [HttpPost("Parcels")]
        public async Task<ActionResult<ParcelBag>> CreateParcelBag(ParcelBagModel bagModel)
        {
            var bag = ParcelBagMapper.MapToDomain(bagModel);
            var newBag = await AppBLL.ParcelBags.Add(bag);
            
            await AppBLL.SaveChangesAsync();
            return Ok(newBag);
        }

        [HttpPost("Parcels/List")]
        public async Task<ActionResult<List<ParcelBag>>> CreateParcelBags(
            List<ParcelBagModel> parcelBagModels)
        {
            var newParcelBag = new List<ParcelBag>();
            foreach (var parcelBagModel in parcelBagModels)
            {
                var parcelBag = ParcelBagMapper.MapToDomain(parcelBagModel);
                newParcelBag.Add(await AppBLL.ParcelBags.Add(parcelBag));
            }
            
            await AppBLL.SaveChangesAsync();
            return Ok(newParcelBag);
        }
    }
}