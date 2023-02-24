using ClothesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repository;
using Repository.Services;
using System.Diagnostics;

namespace ClothesWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _service;

        public HomeController(ILogger<HomeController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _service.GetAll();
            return View(products);
        }

        public IActionResult Details(string productId)
        {
            var pro = _service.GetAll().Where(d => d.Id.ToString() == productId).FirstOrDefault(); 
            return View(pro);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}