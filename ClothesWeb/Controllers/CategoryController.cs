using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repository;
using Repository.Services;

namespace ClothesWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _cateService;

        public CategoryController(ICategoryService cateService)
        {
            _cateService = cateService;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> cate = _cateService.GetAll();
            return View(cate);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _cateService.Create(category);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            var cate = _cateService.GetAll().Where(d => d.Id.ToString() == id).FirstOrDefault();
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Category cate)
        {
            _cateService.Update(cate);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            var doc = _cateService.GetAll().Where(d => d.Id.ToString() == id).FirstOrDefault();
            _cateService.Delete(doc);
            return RedirectToAction("Index");
        }
    }
}

