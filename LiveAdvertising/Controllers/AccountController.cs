using LiveAdvertising.Models;
using LiveAdvertising.Models.Dto;
using LiveAdvertising.Models.Entities;
using LiveAdvertising.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LiveAdvertising.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext context;

        public AccountController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                Shop shop = await context.Shops.Where(x => x.Name == model.Name).FirstOrDefaultAsync();

                if (shop != null)
                    ModelState.AddModelError("", $"Магазин \"{model.Name}\" уже зарегестрирован");
                else
                {
                    shop.Name = model.Name;
                    shop.Email = model.Email;
                    shop.Password = Cryptography.EncryptPassword(model.Password);

                    await context.Shops.AddAsync(shop);

                    await context.SaveChangesAsync();

                    await Authenticate(shop, model.RememberMe);

                    return Redirect("/");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                string encryptedPassword = Cryptography.EncryptPassword(model.Password);

                Shop shop = await context.Shops.Where(x => x.Name == model.Name && x.Password == encryptedPassword).FirstOrDefaultAsync();

                if(shop == null)
                {
                    ModelState.AddModelError("", "Неправильное название компании или пароль");
                }
                else
                {
                    await Authenticate(shop, model.RememberMe);

                    return Redirect("/");
                }
            }

            return View(model);
        }

        private async Task Authenticate(Shop shop, bool rememberMe)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, shop.Name),
                new Claim("Email", shop.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "shop")
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            int days = rememberMe ? 14 : 1;

            AuthenticationProperties authenticationProperties = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(days)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id), authenticationProperties);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
