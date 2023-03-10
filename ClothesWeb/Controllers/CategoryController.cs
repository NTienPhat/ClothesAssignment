using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Repository.Models;
using Repository.Repository;
using Repository.Services;
using X.PagedList;

namespace ClothesWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _cateService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService cateService, IMapper mapper)
        {
            _cateService = cateService;
            _mapper = mapper;
        }

        public IActionResult Index(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }
            var pageSize = 10;
            var list = _cateService.GetAll().ToPagedList(page ?? 1, pageSize);
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO category)
        {
            var cate = _mapper.Map<Category>(category);
            _cateService.Create(cate);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            var cate = _cateService.GetAll().Where(d => d.Id.ToString() == id).FirstOrDefault();
            var obj = _mapper.Map<CategoryDTO>(cate);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(CategoryDTO category)
        {
            var cate = _mapper.Map<Category>(category);
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

