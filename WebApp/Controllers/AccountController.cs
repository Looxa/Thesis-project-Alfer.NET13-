﻿using FileSharer.Web.Models;
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
        private IFileService _fileService;
        private AppDBContext _dbContext;

        public AccountController(AppDBContext dbContext, IFileService fileService)

        {
            _fileService = fileService;
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
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RoleName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        public async Task<IActionResult> AdminPanel()
        {
            var entityUsers = _fileService.GetAllUsers();
            UserModel users = new UserModel()
            {
                Users = entityUsers.ToList()
            };
            return View(users);

        }


        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int? id)
        {
            User userToDelete = _fileService.GetUser(id);
            _fileService.DeleteUser(userToDelete);
            return RedirectToAction("AdminPanel", "Account");

        }

        [Authorize(Roles = "Admin")]
        public IActionResult ChangeRole(int? id)
        {
            {
                return View();  //пока не реализовано
            }

        }
    }
}
