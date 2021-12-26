using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace WebApp.Models {
    public class UsersModel {
        public List<UserModel> UserList { get; set; }
        public string UserName { get; set; }
        public List<IdentityRole> RoleList { get; set; }
    }
}
