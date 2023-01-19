using CarSelling.Models;

namespace CarSelling.Infrastructure
{
    public static class MappingMethods
    {
        public static CarAd ToCarAd(this CarAdDto dto)
        {
            return new CarAd
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Negotiable = dto.Negotiable,
                PhotoPath = dto.PhotoPath,
                Owner = dto.Owner
            };
        }

        public static CarAdDto ToCarAdDto(this CarAd ad)
        {
            return new CarAdDto
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                Negotiable = ad.Negotiable,
                PhotoPath = ad.PhotoPath,
                Owner = ad.Owner
            };
        }

        public static CarAdDto? ToCarAdDtoNullable(this CarAd? ad)
        {
            return ad?.ToCarAdDto() ?? null;
        }

        public static UserResponseDto ToUserResponseDto(this UserRequestDto dto, int id)
        {
            return new UserResponseDto
            {
                Id = id,
                UserName = dto.UserName
            };
        }

        public static UserResponseDto? ToUserResponseDto(this User? user)
        {
            return user is null ? null : new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }

        public static User ToUserWithoutPassword(this UserRequestDto dto, int id)
        {
            return new User
            {
                Id = id,
                UserName = dto.UserName
            };
        }

        public static User ToUserWithoutPasswordAndId(this UserRequestDto dto)
        {
            return new User
            {
                UserName = dto.UserName
            };
        }
    }
}
