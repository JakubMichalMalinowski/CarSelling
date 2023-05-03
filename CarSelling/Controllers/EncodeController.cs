using CarSelling.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSelling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncodeController : ControllerBase
    {
        private readonly IEncodeService _service;

        public EncodeController(IEncodeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Encode(IFormFile file)
        {
            string encodedFile = await _service.EncodeFileAsync(file);
            return Ok(encodedFile);
        }
    }
}
