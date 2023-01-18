using CarSelling.Exceptions;
using CarSelling.Infrastructure;
using CarSelling.Models;
using CarSelling.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarSelling.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id) =>
            (await _repository.GetUserByIdAsync(id)).ToUserResponseDto();

        public async Task<string> LoginUserAsync(UserRequestDto userDto)
        {
            var user = await _repository.GetUserWithUserNameAsync(userDto.UserName);
            if (user is null)
            {
                throw new BadCredentialsException();
            }

            var passwordHasher = new PasswordHasher<User>();
            if (passwordHasher.VerifyHashedPassword(
                user, user.HashedPassword, userDto.Password)
                == PasswordVerificationResult.Failed)
            {
                throw new BadCredentialsException();
            }

            var issuer = _configuration["JwtSettings:iss"];
            var audience = _configuration["JwtSettings:aud"];
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }

        public async Task<UserResponseDto> RegisterUserAsync(UserRequestDto userRequestDto)
        {
            if (await _repository.UserWithUserNameExistsAsync(userRequestDto.UserName))
            {
                throw new UserAlreadyExistsException();
            }

            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                UserName = userRequestDto.UserName
            };

            user.HashedPassword = passwordHasher
                .HashPassword(user, userRequestDto.Password);

            await _repository.CreateUserAsync(user);

            return userRequestDto.ToUserResponseDto(user.Id);
        }
    }
}
