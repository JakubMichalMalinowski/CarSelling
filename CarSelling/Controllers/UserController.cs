using CarSelling.Exceptions;
using CarSelling.Infrastructure;
using CarSelling.Models;
using CarSelling.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarSelling.Controllers
{
    [Authorize]
    [JwtValidation]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [DisableJwtValidation]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(UserRequestDto userRequestDto)
        {
            var userResponseDto = await _service.RegisterUserAsync(userRequestDto);
            return CreatedAtAction(nameof(Get), new { id = userResponseDto.Id }, userResponseDto);
        }

        [AllowAnonymous]
        [DisableJwtValidation]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            var result = await _service.LoginUserAsync(userDto);
            return Ok(result);
            //return Ok(new { token = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserRequestDto dto)
        {
            await _service.UpdateUserAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
