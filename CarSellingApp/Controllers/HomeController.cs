using CarSelling.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarSellingApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}