using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ClothesWeb.Extensions;
using ClothesWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DTO;
using Repository.Models;
using Repository.Services;

namespace ClothesWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IOrderdetailService _orderdetailService;
        private readonly IProductService _proService;
        private readonly IMapper _mapper;
        public INotyfService _notifyService { get; }

        public CartController(IWebHostEnvironment webHostEnvironment, ICartService cartService, IProductService proService, IMapper mapper, INotyfService notifyService)
        {
            _webHostEnvironment = webHostEnvironment;
            _cartService = cartService;
            _proService = proService;
            _mapper = mapper;
            _notifyService = notifyService;
        }

        public List<Cart> CurrentCart
        {
            get
            {
                var cart = HttpContext.Session.Get<List<Cart>>("Cart");
                if (cart == default(List<Cart>))
                {
                    cart = new List<Cart>();
                }
                return cart;
            }
        }

        public IActionResult Index()
        {
            return View(CurrentCart);
        }

        [HttpPost]
        [Route("api/Cart/Add")]
        public IActionResult AddToCart([FromBody] AddToCartVM vm)
        {
            List<Cart> cart = CurrentCart;
            try
            {
                Cart item = cart.SingleOrDefault(p => p.Product.Id == vm.ProductId);
                if (item != null)
                {
                    item.Quantity = item.Quantity + 1;
                    HttpContext.Session.Set<List<Cart>>("Cart", cart);
                }
                else
                {
                    Product pro = _proService.GetOne(vm.ProductId);
                    item = new Cart
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Product = pro,
                        ProductId = pro.Id,
                    };
                    cart.Add(item);
                }

                HttpContext.Session.Set<List<Cart>>("Cart", cart);
                _notifyService.Success("Add product successfully");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("api/Cart/Update")]
        public IActionResult UpdateCart([FromBody] List<UpdateCartVM> vm)
        {
            var cart = HttpContext.Session.Get<List<Cart>>("Cart");
            try
            {
                if (cart != null)
                {
                    vm.ForEach(x =>
                    {
                        Cart item = cart.SingleOrDefault(p => p.Product.Id == x.ProductId);
                        if (item != null)
                        {
                            item.Quantity = x.Quantity;
                        }
                    });
                    //Lưu lại session
                    HttpContext.Session.Set<List<Cart>>("Cart", cart);
                    _notifyService.Success("Update cart successfully");
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("api/Cart/DeleteCart")]
        public IActionResult DeleteCart([FromBody] AddToCartVM vm)
        {
            List<Cart> cart = CurrentCart;
            try
            {
                Cart item = cart.SingleOrDefault(p => p.Product.Id == vm.ProductId);
                if (item != null)
                {
                    cart.Remove(item);
                    HttpContext.Session.Set<List<Cart>>("Cart", cart);
                }

                _notifyService.Success("Delete product successfully");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
        //[HttpPost]
        //[Route("api/Cart/Checkout")]
        //public IActionResult Checkout()
        //{
            
        //    var cart = HttpContext.Session.Get<List<Cart>>("Cart");
        //    try
        //    {
        //        Order order = new Order()
        //        {
        //            OrderDate = DateTime.Now,
        //            OrderNumber = String.Format("{0:ddmmyyyyHHmmss}", DateTime.Now)
        //        };
        //        _orderService.Create(order);

        //        Guid orderId = order.Id;
        //        foreach(var item in cart)
        //        {
        //            OrderDetail orderDetail = new OrderDetail();
        //            orderDetail.Total = item.TotalMoney;
        //            orderDetail.ProductId = item.ProductId;
        //            orderDetail.OrderId =   orderId;
        //            orderDetail.Quantity= item.Quantity;
        //            orderDetail.UnitPrice = 0;
        //            _orderdetailService.Create(orderDetail);
        //        }
        //        _notifyService.Success("Checkout successfully");
        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false });
        //    }
        //}

        [HttpPost]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.Get<List<Cart>>("Cart");
            OrderDTO order = new OrderDTO()
            {
                OrderDate = DateTime.Now,
                OrderNumber = String.Format("{0:ddmmyyyyHHmmss}", DateTime.Now)
            };
            var _order = _mapper.Map<Order>(order);
            _orderService.Create(_order);

            Guid orderId = order.Id;
            foreach (var item in cart)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.Total = item.TotalMoney;
                orderDetail.ProductId = item.ProductId;
                orderDetail.OrderId = orderId;
                orderDetail.Quantity = item.Quantity;
                orderDetail.UnitPrice = 0;
                _orderdetailService.Create(orderDetail);
            }
            HttpContext.Session = null;
            return RedirectToAction("Index");
        }

    }
}
