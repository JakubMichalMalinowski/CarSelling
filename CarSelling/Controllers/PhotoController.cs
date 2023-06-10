using CarSelling.Infrastructure;
using CarSelling.Models;
using CarSelling.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

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
        public async IAsyncEnumerable<object> Upload(List<IFormFile> files)
        {
            await foreach (var path in _service.UploadFilesAsync(files))
            {
                yield return new { Path = path };
            }
        }

        [HttpPost("xml")]
        public async Task<IList<string>> UploadXml(List<IFormFile> files)
        {
            var paths = new List<string>();
            await foreach (var path in _service.UploadFilesAsync(files))
            {
                paths.Add(path);
            }
            return paths;
        }

        [AllowAnonymous]
        [DisableJwtValidation]
        [HttpGet("file/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var stream = await _service.DownloadFileAsync(id);            

            return File(stream.PhotoStream, stream.MimeType
                ?? "application/octet-stream");
        }

        [AllowAnonymous]
        [DisableJwtValidation]
        [HttpGet("encoded/{id}")]
        public async Task<MimedString> DownloadEncoded(int id)
        {
            return await _service.DownloadEncodedFileAsync(id);
        }
    }
}
