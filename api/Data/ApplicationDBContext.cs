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

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x=> x.HasKey(p=> new {p.AppUserId,p.StockId}));

            builder.Entity<Portfolio>()
            .HasOne(u=> u.AppUser)
            .WithMany(u=> u.portfolios)
            .HasForeignKey(p=> p.AppUserId);

            builder.Entity<Portfolio>()
            .HasOne(u=> u.Stock)
            .WithMany(u=> u.portfolios)
            .HasForeignKey(p=> p.StockId);

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