using FileSharer.Web.ViewModels;
using FileSharer.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FileSharer.Web.Data.EntityF;
using Microsoft.AspNetCore.Authentication;

namespace FileSharer.Web.Controllers
{
    public class AccountController : Controller
    {
        private AppDBContext _dbContext;

        public AccountController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new User { Email = model.Email, Password = model.Password, FirstName = model.FirstName, LastName = model.LastName };
                    Role userRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == "Guest");
                    if (userRole != null)
                        user.Role = userRole;

                    _dbContext.Users.Add(user);                    
                    await _dbContext.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RoleName)
            };
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }        
    }
}
