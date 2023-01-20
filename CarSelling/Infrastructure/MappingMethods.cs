﻿using CarSelling.Models;
using System.Reflection.Metadata.Ecma335;

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
                PhotoPath = dto.PhotoPath,
                CreatedBy = user
            };
        }

        public static CarAd ToCarAdWithId(this CarAdRequestDto dto, int id)
        {
            return new CarAd
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Negotiable = dto.Negotiable,
                PhotoPath = dto.PhotoPath
            };
        }

        public static CarAdResponseDto ToCarAdResponseDto(this CarAd ad)
        {
            return new CarAdResponseDto
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                Negotiable = ad.Negotiable,
                PhotoPath = ad.PhotoPath
            };
        }

        public static CarAdResponseDto? ToCarAdWithUserResponseDto(this CarAd? ad)
        {
            if (ad is null)
            {
                return null;
            }

            var responseDto = ad.ToCarAdResponseDto();

            responseDto.OwnerUserName = ad.CreatedBy.UserName;
            responseDto.FirstName = ad.CreatedBy.FirstName;
            responseDto.LastName = ad.CreatedBy.LastName;
            responseDto.PhoneNo = ad.CreatedBy.PhoneNo;

            return responseDto;
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
