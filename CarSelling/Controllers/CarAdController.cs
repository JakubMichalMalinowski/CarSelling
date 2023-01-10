using CarSelling.Data;
using CarSelling.Exceptions;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CarAd carAd)
        {
            if (id != carAd.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateCarAdAsync(carAd);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteCarAdAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}