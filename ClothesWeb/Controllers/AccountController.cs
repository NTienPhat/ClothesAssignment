using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Services;
using System.Security.Claims;

namespace ClothesWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            User authenticateUser = await _userService.Authenticate(user.Email, user.Password);

            if(authenticateUser != null)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Role, authenticateUser.Role)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
                HttpContext.Session.SetString("username", "LoggedIn");
                
                if (authenticateUser.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (authenticateUser.Role == "Admin")
                {
                    HttpContext.Session.SetString("role", "Admin");
                    return RedirectToAction("Index", "Category");
                }
            }
            else ViewData["ValidateMessage"] = "Wrong Email or Password";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // check if the email already exists
                User existingUser = _userService.GetAll().FirstOrDefault(u => u.Email == user.Email);

                if (existingUser == null)
                {
                    _userService.Create(user);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Email", "Email already exists");
                }
            }

            // if the model state is not valid, return the registration view with error messages
            return View("Register");
        }
    }
}
