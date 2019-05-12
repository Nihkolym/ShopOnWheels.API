using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShopOnWheels.Domain.Models.Base;
using ShopOnWheels.Domain.Models.Category;
using ShopOnWheels.Domain.Models.Order;
using ShopOnWheels.Domain.Models.Product;
using ShopOnWheels.Domain.Models.ProductList;
using ShopOnWheels.Domain.Models.User;

namespace ShopOnWheels.Domain
{
    public class ShopOnWheelsDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductList> ProductLists { get; set; }

        public ShopOnWheelsDbContext(DbContextOptions<ShopOnWheelsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUsers")//I have to declare the table name, otherwise IdentityUser will be created
                .Property(c => c.ProviderKey).HasMaxLength(36).IsRequired();

            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUsers")//I have to declare the table name, otherwise IdentityUser will be created
                .Property(c => c.LoginProvider).HasMaxLength(36).IsRequired();

            builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens")//I have to declare the table name, otherwise IdentityUser will be created
               .Property(c => c.LoginProvider).HasMaxLength(36).IsRequired();

            builder.Entity<IdentityRole>()
               .Property(c => c.Name).HasMaxLength(36).IsRequired();

            builder.Entity<User>()
              .Property(c => c.UserName).HasMaxLength(36).IsRequired();

            builder.Entity<User>()
               .Property(user => user.Email)
               .HasMaxLength(36);

            builder.Entity<Product>()
                .Property(p => p.IsDeleted)
                .HasColumnType("bit");

            builder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            builder.Entity<User>()
               .Property(p => p.LockoutEnabled)
               .HasColumnType("bit");

            builder.Entity<User>()
               .Property(p => p.TwoFactorEnabled)
               .HasColumnType("bit");

            builder.Entity<User>()
               .Property(p => p.EmailConfirmed)
               .HasColumnType("bit");

            builder.Entity<User>()
               .Property(p => p.PhoneNumberConfirmed)
               .HasColumnType("bit");

            builder.Entity<Order>()
                .Property(o => o.IsDeleted)
                .HasColumnType("bit");

            builder.Entity<Order>()
                .Property(o => o.IsActive)
                .HasColumnType("bit");

            builder.Entity<ProductList>()
               .Property(o => o.IsDeleted)
               .HasColumnType("bit");

            builder.Entity<Category>()
                .Property(c => c.Id)
                .HasColumnType("bit");

            builder.Entity<Category>()
                .Property(c => c.IsDeleted)
                .HasColumnType("bit");

            builder.Entity<Product>()
                .Property(c => c.IsCountable)
                .HasColumnType("bit");

            builder.Entity<Category>()
               .HasIndex(c => c.Name)
               .IsUnique();

            base.OnModelCreating(builder);

        }
    }
}

