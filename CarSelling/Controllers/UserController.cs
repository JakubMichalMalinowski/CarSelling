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
            try
            {
                var userResponseDto = await _service.RegisterUserAsync(userRequestDto);
                return CreatedAtAction(nameof(Get), new { id = userResponseDto.Id }, userResponseDto);
            }
            catch (UserAlreadyExistsException)
            {
                return Conflict();
            }
        }

        [AllowAnonymous]
        [DisableJwtValidation]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(UserRequestDto userRequestDto)
        {
            try
            {
                var result = await _service.LoginUserAsync(userRequestDto);
                return Ok(new { token = result });
            }
            catch (BadCredentialsException)
            {
                return Unauthorized();
            }
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
            try
            {
                await _service.UpdateUserAsync(id, dto);
            }
            catch (UserAlreadyExistsException)
            {
                return Conflict();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteUserAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
