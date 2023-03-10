using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.DTO;
using Repository.Models;
using Repository.Repository;
using Repository.Services;
using X.PagedList;

namespace ClothesWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service,IWebHostEnvironment webHostEnvironment,IMapper mapper)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public IActionResult Index(int? page = 1)
        {
            if(page != null && page<1)
            {
                page = 1;
            }
            var pageSize = 10;
            var list = _service.GetAll().ToPagedList(page ?? 1, pageSize);
            return View(list);
        }

        public IActionResult Create()
        {
            ICategoryService cate = new CategoryService();
            IEnumerable<SelectListItem> IdList = cate.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }
                );
            ViewBag.Category = IdList;

            Dictionary<string, string> sizeList = new Dictionary<string, string>()
            {
                {"S","small"},
                {"M","medium"},
                {"L","large"},
            };

            IEnumerable<SelectListItem> size = sizeList
                .Select(x => new SelectListItem
                {
                    Text = x.Key,
                    Value = x.Value
                }
                );
            ViewBag.Size = size;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductDTO pro, IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, @"images\products");
            var extension = Path.GetExtension(file.FileName);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }
            pro.Image = @"\images\products\" + fileName + extension;
            var product = _mapper.Map<Product>(pro);
            _service.Create(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            Dictionary<string, string> sizeList = new Dictionary<string, string>()
            {
                {"S","small"},
                {"M","medium"},
                {"L","large"},
            };

            IEnumerable<SelectListItem> size = sizeList
                .Select(x => new SelectListItem
                {
                    Text = x.Key,
                    Value = x.Value
                }
                );
            ViewBag.Size = size;

            ICategoryService cate = new CategoryService();
            IEnumerable<SelectListItem> IdList = cate.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }
                );
            ViewBag.Category = IdList;
            var pro = _service.GetAll().Where(d => d.Id.ToString() == id).FirstOrDefault();
            var product = _mapper.Map<ProductDTO>(pro);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductDTO pro, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);


                var oldImagePath = Path.Combine(wwwRootPath, pro.Image.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }


                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                pro.Image = @"\images\products\" + fileName + extension;
            }
            var product = _mapper.Map<Product>(pro);
            _service.Update(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            var pro = _service.GetAll().Where(d => d.Id.ToString() == id).FirstOrDefault();
            _service.Delete(pro);
            return RedirectToAction("Index");
        }
    }
}
