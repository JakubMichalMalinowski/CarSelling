﻿using CarSelling.Models;

namespace CarSelling.Services
{
    public interface ICarAdService
    {
        public IEnumerable<CarAdDto> GetAll();
        public Task<CarAdDto?> GetByIdAsync(int id);
        public Task CreateAsync(CarAdDto carAd);
        public Task UpdateAsync(int id, CarAdDto carAd);
        public Task DeleteAsync(int id);
    }
}
