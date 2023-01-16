using CarSelling.Models;
using CarSelling.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Register(UserDto userDto)
        {
            bool success = await _service.RegisterUser(userDto);
            if (!success)
            {
                return Conflict("This username exists");
            }

            return CreatedAtAction("Login", new { id = userDto.Id }, userDto); //todo hardcoded string to nameof()
        }
    }
}
