using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers {
    public class UsersController : Controller {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager,RoleManager<IdentityRole> manager) {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }

        
        [HttpPost]
       
        public async Task<IActionResult> Submit(UsersModel model, string button,string currentUser,string message,string privilege) {
            try {
                
                if (!_userManager.FindByNameAsync(currentUser).Result.IsBlocked) {
                    
                        foreach (UserModel User in model.UserList) {
                        
                        if (User.IsChecked) {
                            var user = _userManager.FindByNameAsync(User.User.UserName).Result;
                            if (button.Equals("delete")) {
                                if (user.UserName.Equals(currentUser)) { await _signInManager.SignOutAsync(); }
                                await _userManager.DeleteAsync(user);
                                continue;
                            }
                            if (button.Equals("block")) {
                                    user.IsBlocked = true;
                                    if (user.UserName.Equals(currentUser)) {
                                        await _signInManager.SignOutAsync(); }
                                }

                                if (button.Equals("unblock")) {user.IsBlocked = false;}
                                
                                if (button.Equals("sendMessage")) { user.Messeges += "Message from " + currentUser + ":  " + message + "\n"; }
                                if (button.Equals("giveRight")) { await _userManager.AddToRoleAsync(user, privilege); }
                                if (button.Equals("deleteRight")) { await _userManager.RemoveFromRoleAsync(user, privilege); }
                            await _userManager.UpdateAsync(user);

                        }
                        
                    }
                    


                    }
                else {
                    await _signInManager.SignOutAsync();
                }

            }
            catch { await _signInManager.SignOutAsync(); }
            return RedirectToAction("Index", "Home");
        }
        
    }
}
