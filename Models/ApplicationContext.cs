﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models {
    public class ApplicationContext : IdentityDbContext<User> {
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) {
            
            
            
        }
        public DbSet<User> Users { get; set; }
    }
}
