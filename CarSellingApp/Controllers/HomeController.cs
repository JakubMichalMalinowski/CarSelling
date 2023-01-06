using CarSelling.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace CarSellingApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dev()
        {
            IEnumerable<CarAd>? carAds = null;
            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri("https://localhost:7276/api/");
                carAds = await client.GetFromJsonAsync<IEnumerable<CarAd>>("carad");
            }

            if (carAds is not null)
            {
                return View(carAds);
            }

            return BadRequest();
        }
    }
}