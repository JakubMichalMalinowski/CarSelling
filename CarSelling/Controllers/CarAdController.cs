using CarSelling.Exceptions;
using CarSelling.Models;
using CarSelling.Services;
using Microsoft.AspNetCore.Mvc;

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
            await _service.CreateAsync(carAd);
            return CreatedAtAction(nameof(Get), new { id = carAd.Id }, carAd);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CarAd carAd)
        {
            try
            {
                await _service.UpdateAsync(id, carAd);
            }
            catch (BadRequestException)
            {
                return BadRequest();
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
                await _service.DeleteAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}