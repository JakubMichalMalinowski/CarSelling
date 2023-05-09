using CarSelling.Models;

namespace CarSelling.Infrastructure
{
    public static class MappingMethods
    {
        public static CarAd ToCarAdWithUser(this CarAdRequestDto dto, User user)
        {
            return new CarAd
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Negotiable = dto.Negotiable,
                CreatedBy = user,
                Car = dto.ToCar()
            }
            .RetrievePhotos(dto);
        }

        public static CarAd ToCarAdWithIds(this CarAdRequestDto dto, int adId, int carId)
        {
            return new CarAd
            {
                Id = adId,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Negotiable = dto.Negotiable,
                Car = dto.ToCar(carId)
            }
            .RetrievePhotos(dto);
        }

        private static CarAd RetrievePhotos(this CarAd carAd, CarAdRequestDto dto)
        {
            if (dto.PhotoPaths?.Count > 0)
            {
                carAd.PhotoPaths = dto.PhotoPaths;
                return carAd;
            }

            if (dto.EncodedPhotos?.Count > 0)
            {
                carAd.EncodedPhotos = dto.EncodedPhotos;
                return carAd;
            }

            return carAd;
        }

        public static CarAdSimpleResponseDto ToCarAdSimpleResponseDto(this CarAdDto ad)
        {
            (int? id, PhotoLocation location)? mainPhoto = ad.GetMainPhoto();
            return new CarAdSimpleResponseDto
            {
                Id = ad.Id,
                Title = ad.Title,
                Price = ad.Price,
                Negotiable = ad.Negotiable,
                MainPhotoId = mainPhoto?.id,
                MainPhotoLocation = mainPhoto?.location
            };
        }

        private static (int? id, PhotoLocation location)? GetMainPhoto(this CarAdDto ad)
        {
            if (ad.PhotoPathIds?.Length > 0)
            {
                return (ad.PhotoPathIds.FirstOrDefault(), PhotoLocation.FileStorage);
            }

            if (ad.EncodedPhotoIds?.Length > 0)
            {
                return (ad.EncodedPhotoIds.FirstOrDefault(), PhotoLocation.EncodedFile);
            }

            return null;
        }

        public static CarAdResponseDto? ToCarAdResponseDto(this CarAdDto? ad)
        {
            (int[]? ids, PhotoLocation? location)? photos = ad?.GetPhotos();

            return ad is null ? null : new CarAdResponseDto
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                Negotiable = ad.Negotiable,

                OwnerUserName = ad.CreatedBy.UserName,
                FirstName = ad.CreatedBy.FirstName,
                LastName = ad.CreatedBy.LastName,
                PhoneNo = ad.CreatedBy.PhoneNo,

                Make = ad.Car.Make,
                Model = ad.Car.Model,
                ProductionYear = ad.Car.ProductionYear,
                Body = ad.Car.Body,
                FuelType = ad.Car.FuelType,
                EngineCapacity = ad.Car.EngineCapacity,
                Power = ad.Car.Power,
                Mileage = ad.Car.Mileage,
                Drivetrain = ad.Car.Drivetrain,
                Transmission = ad.Car.Transmission,

                PhotoIds = photos?.ids,
                PhotoLocation = photos?.location
            };
        }

        private static (int[]? ids, PhotoLocation? location)? GetPhotos(this CarAdDto ad)
        {
            if (ad.PhotoPathIds?.Length > 0)
            {
                return (ad.PhotoPathIds,
                    PhotoLocation.FileStorage);
            }

            if (ad.EncodedPhotoIds?.Length > 0)
            {
                return (ad.EncodedPhotoIds,
                    PhotoLocation.EncodedFile);
            }

            return null;
        }

        private static Car ToCar(this CarAdRequestDto dto)
        {
            return new Car
            {
                Make = dto.Make,
                Model = dto.Model,
                ProductionYear = dto.ProductionYear,
                Body = dto.Body,
                FuelType = dto.FuelType,
                EngineCapacity = dto.EngineCapacity,
                Power = dto.Power,
                Mileage = dto.Mileage,
                Drivetrain = dto.Drivetrain,
                Transmission = dto.Transmission
            };
        }

        private static Car ToCar(this CarAdRequestDto dto, int carId)
        {
            var car = dto.ToCar();
            car.Id = carId;
            return car;
        }

        public static UserResponseDto ToUserResponseDto(this UserRequestDto dto, int id)
        {
            return new UserResponseDto
            {
                Id = id,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNo = dto.PhoneNo
            };
        }

        public static UserResponseDto? ToUserResponseDto(this User? user)
        {
            return user is null ? null : new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNo = user.PhoneNo
            };
        }

        public static User ToUserWithoutPassword(this UserRequestDto dto, int id)
        {
            return new User
            {
                Id = id,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNo = dto.PhoneNo
            };
        }

        public static User ToUserWithoutPasswordAndId(this UserRequestDto dto)
        {
            return new User
            {
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNo = dto.PhoneNo
            };
        }
    }
}
