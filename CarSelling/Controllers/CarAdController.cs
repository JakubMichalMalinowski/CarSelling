using CarSelling.Data;
using CarSelling.Models;
using CarSelling.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarAdController : ControllerBase
    {
        private readonly ICarAdService _service;

        public CarAdController(ICarAdService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<CarAd> Get() => _service.GetAll();

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ad = await _service.GetByIdAsync(id);
            return ad is not null ? Ok(ad) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarAd carAd)
        {
            await _service.CreateCarAdAsync(carAd);
            return CreatedAtAction(nameof(Get), new { id = carAd.Id }, carAd);
        }

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, CarAd carAd)
        //{
        //    if (id != carAd.Id)
        //    {
        //        return BadRequest();
        //    }

        //    if (!OwnerExists(carAd.Owner.Id))
        //    {
        //        return NotFound();
        //    }

        //    _context.Entry(carAd).State = EntityState.Modified;
        //    _context.Entry(carAd.Owner).State = EntityState.Modified;
            
        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AdExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var ad = _context.CarAds.Find(id);

        //    if (ad is null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CarAds.Remove(ad);
        //    _context.SaveChanges();

        //    return NoContent();
        //}

        //private bool AdExists(int id) =>
        //    _context.CarAds.Any(ad => ad.Id == id);

        //private bool OwnerExists(int id) =>
        //    _context.Owners.Any(owner => owner.Id == id);
    }
}