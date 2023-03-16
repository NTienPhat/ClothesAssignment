using AutoMapper;
using ClothesWeb.Models;
using ClothesWeb.Pagination;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Repository.Repository;
using Repository.Services;
using System.Diagnostics;
using X.PagedList;

namespace ClothesWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _service;
        private readonly ICategoryService _cateService;

        public HomeController(ILogger<HomeController> logger, IProductService service, ICategoryService categoryService)
        {
            _logger = logger;
            _service = service;
            _cateService = categoryService;
        }

        //public async Task<IActionResult> Index(string?size, string? cate, string? PriceSort, int? page = 1)
        //{
        //    //ViewData["PriceSort"] = sort == "price_asc" ? "price_desc" : "price_asc";
        //    //ViewData["LowPrice"] = sort == "LowPrice" ? "price_desc" : "price_asc";
        //    ViewBag.HighPrice = "desc";
        //    ViewBag.LowPrice = "asc";

        //    ViewBag.S = "S";
        //    ViewBag.M = "M";
        //    ViewBag.L = "L";

        //    var cateList = _cateService.GetAll();
        //    ViewBag.Category = cateList;
        //    if (page != null && page < 1)
        //    {
        //        page = 1;
        //    }
        //    var pageSize = 8;
        //    var list = from p in _service.GetAll()
        //               select p;

        //    if (cate != null)
        //    {
        //        list = list.Where(p => p.CategoryId.ToString() == cate.ToString());
        //    }

        //    switch (PriceSort)
        //    {
        //        case "desc":
        //            list = list.OrderByDescending(s => s.Price);
        //            break;
        //        case "asc":
        //            list = from p in list
        //                   orderby p.Price ascending
        //                   select p;
        //            break;
        //    }

        //    switch (PriceSort)
        //    {
        //        case "S":
        //            list = list.Where(p =>p.Size == "S");
        //            break;
        //        case "M":
        //            list = list.Where(p => p.Size == "M");
        //            break;
        //        case "L":
        //            list = list.Where(p => p.Size == "L");
        //            break;
        //    }

        //    return View(await PaginatedList<Product>.CreateAsync(list.AsQueryable<Product>(), page ?? 1, pageSize));
        //    //return View(await PaginatedList<Product>.CreateAsync(list.AsQueryable<Product>(), page ?? 1, pageSize));
        //    //return View(list.ToPagedList(page ?? 1, pageSize));
        //}

        public async Task<IActionResult> Index(IFormCollection formCollection,string? searchString, string ?cate, int? page = 1)
        {
            var cateList = _cateService.GetAll();
            ViewBag.Category = cateList;
            if (page != null && page < 1)
            {
                page = 1;
            }
            var pageSize = 8;
            var list = from p in _service.GetAll()
                       select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.ProductName.Contains(searchString));
            }
            if (cate != null)
            {
                list = list.Where(p => p.CategoryId.ToString() == cate.ToString());
            }
            if (!string.IsNullOrEmpty(formCollection["high"])) {
                list = list.OrderByDescending(s => s.Price);
            }
            if (!string.IsNullOrEmpty(formCollection["low"])) {
                list = from p in list
                       orderby p.Price ascending
                       select p;
            }
            if (!string.IsNullOrEmpty(formCollection["s"]))
            {
                list = from p in list
                       where p.Size =="small"
                       select p;
            }
            if (!string.IsNullOrEmpty(formCollection["m"]))
            {
                list = from p in list
                       where p.Size == "medium"
                       select p;
            }
            if (!string.IsNullOrEmpty(formCollection["l"]))
            {
                list = from p in list
                       where p.Size == "large"
                       select p;
            }
            foreach(var category in cateList)
            {
                if (!string.IsNullOrEmpty(formCollection[$"{category.Id}"]))
                {
                    list = from p in list
                           where p.CategoryId.ToString() == $"{category.Id}"
                           select p;
                }
            }

            return View(await PaginatedList<Product>.CreateAsync(list.AsQueryable<Product>(), page ?? 1, pageSize));
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
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}