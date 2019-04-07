using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopOnWheels.Domain;

namespace ShopOnWheels.Services.Extensions
{
    public static class DatabaseInitializer
    {
        public static async Task EnsureDatabaseInitialized(IServiceScope serviceScope)
        {
            ShopOnWheelsDbContext context = serviceScope.ServiceProvider.GetRequiredService<ShopOnWheelsDbContext>();
            context.Database.Migrate();

            await context.SaveChangesAsync();
            context.Dispose();
        }
    }
}
