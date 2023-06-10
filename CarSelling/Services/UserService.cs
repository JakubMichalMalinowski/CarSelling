using CarSelling.Exceptions;
using CarSelling.Infrastructure;
using CarSelling.Models;
using CarSelling.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarSelling.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICarAdRepository _carAdRepository;
        private readonly ICarAdService _carAdService;
        private readonly IConfiguration _configuration;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserPrincipal _userPrincipal;

        public UserService(IUserRepository userRepository,
            ICarAdRepository carAdRepository,
            ICarAdService carAdService,
            IConfiguration configuration,
            IAuthorizationService authorizationService,
            UserPrincipal userPrincipal)
        {
            _userRepository = userRepository;
            _carAdRepository = carAdRepository;
            _carAdService = carAdService;
            _configuration = configuration;
            _authorizationService = authorizationService;
            _userPrincipal = userPrincipal;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id) ?? throw new NotFoundException();
            var authResult = await _authorizationService.AuthorizeAsync(
                _userPrincipal.UserClaimsPrincipal,
                user,
                new ResourceOwnerRequirement());

            if (!authResult.Succeeded)
            {
                throw new ForbidException();
            }

            var carAdIds = await _carAdRepository
                .GetAllCarAdsIdsCreatedByUserWithIdAsync(id);
            foreach (var carAdId in carAdIds)
            {
                await _carAdService.DeleteCarAdAsync(carAdId);
            }

            await _userRepository.DeleteUserAsync(user);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var authResult = await _authorizationService.AuthorizeAsync(
                _userPrincipal.UserClaimsPrincipal,
                user, new ResourceOwnerRequirement());
            if (!authResult.Succeeded)
            {
                throw new ForbidException();
            }

            return user.ToUserResponseDto();
        }

        public async Task<string> LoginUserAsync(UserLoginDto userDto)
        {
            var user = await _userRepository.GetUserWithUserNameAsync(
                userDto.UserName) ?? throw new BadCredentialsException();
            var passwordHasher = new PasswordHasher<User>();
            if (passwordHasher.VerifyHashedPassword(
                user, user.HashedPassword, userDto.Password)
                == PasswordVerificationResult.Failed)
            {
                throw new BadCredentialsException();
            }

            var issuer = _configuration["JwtSettings:iss"];
            var audience = _configuration["JwtSettings:aud"];
            var key = Encoding.ASCII.GetBytes(
                _configuration["JwtSettings:key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub,
                    user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(double.Parse(
                    _configuration["JwtSettings:exp"]!)),
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
            if (await _userRepository.UserWithUserNameExistsAsync(userRequestDto.UserName))
            {
                throw new UserAlreadyExistsException();
            }

            var passwordHasher = new PasswordHasher<User>();

            var user = userRequestDto.ToUserWithoutPasswordAndId();

            user.HashedPassword = passwordHasher
                .HashPassword(user, userRequestDto.Password);

            await _userRepository.CreateUserAsync(user);

            return userRequestDto.ToUserResponseDto(user.Id);
        }

        public async Task UpdateUserAsync(int id, UserRequestDto userDto)
        {
            var userFromDB = await _userRepository.GetUserByIdAsync(id);
            var authResult = await _authorizationService.AuthorizeAsync(
                _userPrincipal.UserClaimsPrincipal,
                userFromDB,
                new ResourceOwnerRequirement());
            if (!authResult.Succeeded)
            {
                throw new ForbidException();
            }

            if (userFromDB?.UserName != userDto.UserName)
            {
                var userNameExists = await _userRepository
                    .UserWithUserNameExistsAsync(userDto.UserName);

                if (userNameExists)
                {
                    throw new UserAlreadyExistsException();
                }
            }

            _userRepository.DetachUser(userFromDB);

            var passwordHasher = new PasswordHasher<User>();
            var user = userDto.ToUserWithoutPassword(id);
            var hashedPassword = passwordHasher
                .HashPassword(user,
                userDto.Password);
            user.HashedPassword = hashedPassword;

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
