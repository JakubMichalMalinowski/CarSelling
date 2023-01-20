using CarSelling.Exceptions;
using CarSelling.Infrastructure;
using CarSelling.Models;
using CarSelling.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSelling.Controllers
{
    [Authorize]
    [JwtValidation]
    [Route("api/[controller]")]
    [ApiController]
    public class CarAdController : ControllerBase
    {
        private readonly ICarAdService _service;

        public CarAdController(ICarAdService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [DisableJwtValidation]
        [HttpGet]
        public async Task<IEnumerable<CarAdSimpleResponseDto>> Get() => await _service.GetAllCarAdsAsync();

        [AllowAnonymous]
        [DisableJwtValidation]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ad = await _service.GetCarAdByIdAsync(id);
            return ad is not null ? Ok(ad) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarAdRequestDto carAdDto)
        {
            var carAdSimpleResponseDto = await _service.CreateCarAdAsync(carAdDto);
            return CreatedAtAction(nameof(Get), new { id = carAdSimpleResponseDto.Id }, carAdSimpleResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CarAdRequestDto carAdDto)
        {
            await _service.UpdateCarAdAsync(id, carAdDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCarAdAsync(id);
            return NoContent();
        }
    }
}