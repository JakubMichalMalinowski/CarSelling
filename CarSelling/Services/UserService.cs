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

        public async Task DeleteUserAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user is null)
            {
                throw new NotFoundException();
            }

            await _repository.DeleteUserAsync(user);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id) =>
            (await _repository.GetUserByIdAsync(id)).ToUserResponseDto();

        public async Task<string> LoginUserAsync(UserLoginDto userDto)
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
                    new Claim("userId", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(double.Parse(_configuration["JwtSettings:exp"]!)),
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

            var user = userRequestDto.ToUserWithoutPasswordAndId();

            user.HashedPassword = passwordHasher
                .HashPassword(user, userRequestDto.Password);

            await _repository.CreateUserAsync(user);

            return userRequestDto.ToUserResponseDto(user.Id);
        }

        public async Task UpdateUserAsync(int id, UserRequestDto userDto)
        {
            var userNameExists = await _repository
                .UserWithUserNameExistsAsync(userDto.UserName);
            if (userNameExists)
            {
                throw new UserAlreadyExistsException();
            }

            var passwordHasher = new PasswordHasher<User>();
            var user = userDto.ToUserWithoutPassword(id);
            var hashedPassword = passwordHasher
                .HashPassword(user,
                userDto.Password);
            user.HashedPassword = hashedPassword;

            await _repository.UpdateUserAsync(user);
        }
    }
}
