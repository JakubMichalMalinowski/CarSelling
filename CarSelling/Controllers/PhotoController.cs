using CarSelling.Infrastructure;
using CarSelling.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSelling.Controllers
{
    [Authorize]
    [JwtValidation]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _service;

        public PhotoController(IPhotoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async IAsyncEnumerable<string> Upload(List<IFormFile> files)
        {
            await foreach (var path in _service.UploadFilesAsync(files))
            {
                yield return path;
            }
        }
    }
}
