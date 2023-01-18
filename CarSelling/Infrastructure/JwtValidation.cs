using CarSelling.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarSelling.Infrastructure
{
    public class JwtValidation
    {
        private static TokenValidationParameters _tokenValidationParameters = default!;

        public  static void SetTokenValidationParameters(
            IConfiguration configuration)
        {
            _tokenValidationParameters = new()
            {
                ValidIssuer = configuration["JwtSettings:iss"],
                ValidAudience = configuration["JwtSettings:aud"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:key"]!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        }

        public static TokenValidationParameters TokenValidationParameters
        {
            get
            {
                if (_tokenValidationParameters is null)
                {
                    throw new NullFieldException();
                }
                return _tokenValidationParameters;
            }
        }
    }
}
