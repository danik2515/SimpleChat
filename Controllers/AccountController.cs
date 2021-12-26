using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;
using System;
using System.Collections.Generic;

namespace WebApp.Controllers {
    public class AccountController : Controller {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
        [HttpGet]
        public IActionResult Register() {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                User user = new User { Email = model.Email, UserName = model.UserName, DataTimeRegistration = DateTime.Now, DataTimeLogin = DateTime.Now,
                    IsBlocked = false,Messeges=new string("") };
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, "User");
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Index", "Home");
                }
                else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null) {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                try {
                    if (!_userManager.FindByNameAsync(model.UserName).Result.IsBlocked) {
                        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                        if (result.Succeeded) {

                            var user = _userManager.FindByNameAsync(model.UserName).Result;
                            user.DataTimeLogin = DateTime.Now;
                            await _userManager.UpdateAsync(user);
                            return RedirectToAction("Index", "Home");
                        }
                        else {
                            ModelState.AddModelError("", "Incorrect password");
                        }
                    } else {
                        ModelState.AddModelError("", "You are blocked!");

                    }

                }
                catch {
                    ModelState.AddModelError("", "Incorrect User name");
                }
                }
            return View(model);
        }
        [HttpPost]
        
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Index() {

            return View();
        }
       
    }
}
