using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShopOnWheels.Domain.Models.Base;
using ShopOnWheels.Domain.Models.Order;

namespace ShopOnWheels.Domain
{
    public class ShopOnWheelsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ShopOnWheelsDbContext(DbContextOptions<ShopOnWheelsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(p => p.IsDeleted)
                .HasColumnType("bit");
        }


    }
}

