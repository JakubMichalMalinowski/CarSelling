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
        public IActionResult Encode(List<IFormFile> files)
        {
            IList<string> encodedFiles = _service.EncodeFiles(files);
            return Ok(encodedFiles);
        }
    }
}
