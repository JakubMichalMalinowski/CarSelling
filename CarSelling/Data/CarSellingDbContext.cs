﻿using CarSelling.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Data
{
    public class CarSellingDbContext : DbContext
    {
        public CarSellingDbContext(DbContextOptions<CarSellingDbContext> options) : base(options) { }

        public DbSet<CarAd> CarAds { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
