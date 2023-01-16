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
    }
}
