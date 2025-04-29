using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCore_Learning.Models;

namespace NetCore_Learning.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<UserStock> UserStock { get; set; }
        public DbSet<Org> Org { get; set; }
        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     base.OnModelCreating(builder);
        //     List<IdentityRole> roles = new List<IdentityRole>{
        //         new IdentityRole{
        //             Id = "admin-role",
        //             Name = "Admin",
        //             NormalizedName = "ADMIN",
        //         },
        //           new IdentityRole{
        //             Id = "user-role",
        //             Name = "User",
        //             NormalizedName = "USER",
        //         },
        //     };
        //     builder.Entity<IdentityRole>().HasData(roles);
        // }
    }
}