using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopOnWheels.Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ShopOnWheels.Services.Helpers;
using ShopOnWheels.Domain.Identity;
using ShopOnWheels.Domain.Models.User;

namespace ShopOnWheels.Services.Extensions
{
    public static class DatabaseInitializer
    {

        public static async Task EnsureDatabaseInitialized(IServiceScope serviceScope)
        {
            ShopOnWheelsDbContext context = serviceScope.ServiceProvider.GetRequiredService<ShopOnWheelsDbContext>();

            bool isFirstLaunch = !(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();

            context.Database.Migrate();

            if (isFirstLaunch)
            {
                await SeedRoles(context);
                await SeedAdmin(serviceScope);
            }

            await context.SaveChangesAsync();
            context.Dispose();
        }

        public static async Task SeedRoles(ShopOnWheelsDbContext context)
        {
            var userRole = Roles.User;
            var adminRole = Roles.Admin;

            await context.AddRangeAsync(
                new IdentityRole[] {
                    new IdentityRole { Name = userRole, NormalizedName = userRole.ToUpper() },
                    new IdentityRole { Name = adminRole, NormalizedName = adminRole.ToUpper() }
                }
            );
        }

        public static async Task SeedAdmin(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

            const string email = Constants.Strings.Admin.Email;
            const string password = Constants.Strings.Admin.Password;
            var role = Roles.Admin;

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                User user = new User
                {
                    UserName = email,
                    Email = email,
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }

        }

        public static async Task WarmUpContext(IServiceScope serviceScope)
        {
            ShopOnWheelsDbContext context = serviceScope.ServiceProvider.GetRequiredService<ShopOnWheelsDbContext>();

            await context.ProductLists
                .Include(pl => pl.Order)
                .FirstOrDefaultAsync();

            await context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync();
        }

        public static async Task AddTestData(ShopOnWheelsDbContext context)
        {

        }
    }
}
