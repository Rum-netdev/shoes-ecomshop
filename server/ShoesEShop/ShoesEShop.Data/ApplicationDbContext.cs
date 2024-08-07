using ShoesEShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesEShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<
        AppUser,
        AppRole,
        int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>()
                .ToTable("Users")
                .HasKey(e => e.Id);

            builder.Entity<AppRole>()
                .ToTable("Roles")
                .HasKey(e => e.Id);

            builder.Entity<IdentityUserClaim<int>>()
                .ToTable("UserClaims").HasKey(t => t.Id);

            builder.Entity<IdentityRoleClaim<int>>()
                .ToTable("RoleClaims").HasKey(t => t.Id);

            builder.Entity<IdentityUserRole<int>>()
                .ToTable("UserRoles").HasKey(t => new {t.UserId, t.RoleId});

            builder.Entity<IdentityUserLogin<int>>()
                .ToTable("UserLogins").HasKey(t => t.UserId);

            builder.Entity<IdentityUserToken<int>>()
                .ToTable("UserTokens").HasKey(t => t.UserId);
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
    }
}
