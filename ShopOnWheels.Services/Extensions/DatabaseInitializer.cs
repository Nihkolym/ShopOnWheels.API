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
                await AddTestData(context);
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
            await context.Database.ExecuteSqlCommandAsync($@"INSERT INTO `categories` (`Id`, `Name`, `IsDeleted`, `CreatedOn`, `ModifiedOn`) VALUES
                    ('ath99ed6-0c-4d0e', 'milk products', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873'),
                    ('btk94ed6-0c-4d0e', 'meat', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873'),
                    ('atl96ed6-0c-450e', 'vegetables', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873'),
		            ('ath99md6-0c-4d0e', 'fruits', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873'),
		            ('ath9ved6-0c-5d0e', 'bakery', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873'),
		            ('asd99ed6-1c-4d0e', 'drinks', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873'),
	                ('csd34ed6-2c-4d0e', 'sweets', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873');");

            await context.Database.ExecuteSqlCommandAsync($@"INSERT INTO `products` (`Id`, `Name`, `IsDeleted`, `CreatedOn`, `ModifiedOn`, `Weight`, `Manufacturer`, `Price`, `IsCountable`, `CategoryId`) VALUES
                    ('mfkr74hf-3f-4d0e', 'milk', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', '500', 'Galicia', '12.50', b'1', 'ath99ed6-0c-4d0e'),
                    ('btk94ed6-2h-4d0e', 'cheese', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', null, 'Romol', '45', b'0', 'ath99ed6-0c-4d0e'),
                    ('atlp6ed6-0c-450e', 'orange', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', null, 'Nashe', '23', b'0', 'ath99md6-0c-4d0e'),
		            ('atl19md6-9c-4d0e', 'strawberry', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', null, 'Nasha Fabrika', '78', b'0', 'ath99md6-0c-4d0e'),
		            ('ath6ved6-1j-5d0e', 'chiken', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', '500', 'Dom', '85.52', b'1', 'btk94ed6-0c-4d0e'),
		            ('az349ed6-d4-4d0e', 'bacon', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', null, 'Dom Fab', '85.52', b'0', 'btk94ed6-0c-4d0e'),
	                ('qbnk1ed0-b9-4d0e', 'water', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', '500', 'Morshinska', '11', b'1', 'asd99ed6-1c-4d0e'),
 		            ('mfwv74hf-2f-4d0e', 'pepsi', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', '500', 'PepsiCo', '14.50', b'1', 'asd99ed6-1c-4d0e'),
                    ('btl94ed6-2h-4d0e', 'oreo', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', '100', 'Nabisco', '15', b'1', 'csd34ed6-2c-4d0e'),
                    ('atr76ed6-0c-450e', 'chocolate', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873','95', 'Milka', '25', b'1', 'csd34ed6-2c-4d0e'),
		            ('atns9md6-9c-4d0e', 'bread', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873','450', '8 merchanised bakery', '16.75', b'1', 'ath9ved6-0c-5d0e'),
		            ('ath2fmd6-3j-5d0e', 'bagels', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', null, '8 merchanised bakery', '25', b'0', 'ath9ved6-0c-5d0e'),
		            ('azy99e7h-l4-4d0e', 'tomatoes', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', null, 'Gifts of the nature', '45', b'0', 'atl96ed6-0c-450e'),
	                ('qbd31emk-n9-4d0e', 'cabbages', b'0', '2018-05-13 18:25:38.242873', '2018-05-13 18:25:38.242873', '500', 'Gifts of the nature', '130', b'1', 'atl96ed6-0c-450e');");
        }
    }
}
