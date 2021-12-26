using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers {
    public class HomeController : Controller {
        private readonly ApplicationContext _contex;
        RoleManager<IdentityRole> _roleManager;
        public HomeController(ApplicationContext contex, RoleManager<IdentityRole> manager) {
            _roleManager = manager;
            _contex = contex;
        }

        public async Task<IActionResult> Index() {
            if (!await _roleManager.RoleExistsAsync("Admin")) {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }
            if (!await _roleManager.RoleExistsAsync("User")) {
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }
                
            
            
            var users = _contex.Users.ToArray();
            UsersModel model = new UsersModel();
            model.UserList = new List<UserModel>();
            foreach (var user in users) {
                model.UserList.Add(new UserModel() { User = user, IsChecked = false });
            }
            model.RoleList = _roleManager.Roles.ToList();
            return View(model);
            
        }
 
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return RedirectToAction("Index", "Home");
        }
        

    }
}
