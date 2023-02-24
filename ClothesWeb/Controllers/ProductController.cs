using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Repository.Repository;
using Repository.Services;

namespace ClothesWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductService _service;

        public ProductController(IProductService service,IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var list = _service.GetAll();
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
        public IActionResult Create(Product pro, IFormFile file)
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
            _service.Create(pro);
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
            return View(pro);
        }

        [HttpPost]
        public IActionResult Edit(Product pro, IFormFile? file)
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
            _service.Update(pro);
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
