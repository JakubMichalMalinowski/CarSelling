using CarSelling.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        public IActionResult HandleError()
        {
            var handler = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = handler?.Error;

            if (ex is NotFoundException) return NotFound();
            if (ex is UserAlreadyExistsException) return Conflict();
            if (ex is BadCredentialsException) return Unauthorized();
            if (ex is BadRequestException) return BadRequest();
            if (ex is UnauthorizedException) return Unauthorized();

            return Problem();
        }
    }
}
