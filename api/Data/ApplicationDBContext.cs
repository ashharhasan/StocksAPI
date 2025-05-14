using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Stock> Stocks { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> identityRoles = new List<IdentityRole>();

            identityRoles.Add(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            identityRoles.Add(new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            });
            builder.Entity<IdentityRole>().HasData(identityRoles);
        }
    }
}