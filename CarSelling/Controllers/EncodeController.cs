using CarSelling.Models;
using CarSelling.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSelling.Controllers
{
    [Route("api/[action]")]
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
            var encodedFiles = _service.EncodeFiles(files);
            return Ok(encodedFiles);
        }

        [HttpPost]
        public IActionResult Decode(MimedString encodedFileWithType)
        {
            return File(_service.DecodeFile(encodedFileWithType.Content),
                encodedFileWithType.Type);
        }
    }
}
