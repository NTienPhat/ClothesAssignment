using ClothesWeb.Extensions;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;

namespace ClothesWeb.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<Cart>>("Cart");
            return View(cart);
        }
    }
}
