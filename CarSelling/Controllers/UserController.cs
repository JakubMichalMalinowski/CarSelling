﻿using CarSelling.Exceptions;
using CarSelling.Models;
using CarSelling.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarSelling.Controllers
{
    [Authorize]
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
    }
}
