using CarSelling.Data;
using CarSelling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarAdController : ControllerBase
    {
        private readonly CarSellingDbContext _context;
        public CarAdController(CarSellingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<CarAd> Get() => _context.CarAds.Include(ca => ca.Owner);
    }
}
