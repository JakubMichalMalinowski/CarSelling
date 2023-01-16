using CarSelling.Models;
using CarSelling.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarSelling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(UserCreationDto userCreationDto)
        {
            bool success = await _service.RegisterUserAsync(userCreationDto);
            if (!success)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(Register), userCreationDto); //todo
        }
    }
}
