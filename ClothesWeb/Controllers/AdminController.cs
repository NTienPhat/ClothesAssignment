using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.Services;
using X.PagedList;

namespace ClothesWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _UserService;
        private readonly IOrderService _OrderService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService, IOrderService orderService, IMapper mapper)
        {
            _UserService = userService;
            _OrderService = orderService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DetailsUser(Guid id)
        {
            var user = _UserService.GetAll().Where(x => x.Id == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        [Route("api/Admin/Delete")]
        public IActionResult DeleteUser(string id)
        {
            var obj = _UserService.GetAll().Where(d => d.Id.ToString() == id).FirstOrDefault();
            _UserService.Delete(obj);
            return RedirectToAction("Index");
        }

        public IActionResult SearchUser(string keyword) { 
            var obj = _UserService.SearchByKeyword(keyword);
            if(obj != null)
            {
                return View(obj);
            }
            return View();
        }

        [HttpPost]
        [Route("api/Order/Delete")]
        public IActionResult DeleteOrder(string id)
        {
            var obj = _OrderService.GetAll().Where(d => d.Id.ToString() == id).FirstOrDefault();
            _OrderService.Delete(obj);
            return RedirectToAction("Index");
        }

        public IActionResult SearchOrder(string keyword)
        {
            var obj = _OrderService.SearchByKeyword(keyword);
            if (obj != null)
            {
                return View(obj);
            }
            return View();
        }
    }
}
