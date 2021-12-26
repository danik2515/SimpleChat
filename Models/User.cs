using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebApp.Models {
    public class User : IdentityUser {
        public DateTime DataTimeRegistration { get; set; }
        public DateTime DataTimeLogin { get; set; }
        
        public bool IsBlocked { get; set; }
        public string Messeges { get; set; }
       
    }
}
