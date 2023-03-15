using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.Services;
using X.PagedList;

namespace ClothesWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IManagementUserService _mgUserService;
        private readonly IMapper _mapper;

        public AdminController(IManagementUserService mgUserService, IMapper mapper)
        {
            _mgUserService = mgUserService;
            _mapper = mapper;
        }

        public IActionResult Index(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }
            var pageSize = 10;
            var list = _mgUserService.GetAll().ToPagedList(page ?? 1, pageSize);
            return View(list);
        }

        public IActionResult Delete(string id)
        {
            var doc = _mgUserService.GetAll().Where(d => d.Id.ToString() == id).FirstOrDefault();
            _mgUserService.Delete(doc);
            return RedirectToAction("Index");
        }
    }
}
