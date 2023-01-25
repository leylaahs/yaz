﻿using eBusiness.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eBusiness.DAL
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions options):base(options) { }
        public DbSet<Position> Positions { get; set; } 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AppUser> AppUsers{ get; set; }
    }
}
