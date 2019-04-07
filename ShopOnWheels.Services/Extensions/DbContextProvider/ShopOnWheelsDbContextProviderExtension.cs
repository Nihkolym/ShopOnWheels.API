using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopOnWheels.Domain;

namespace ShopOnWheels.Services.Extensions.DbContextProvider
{
    public static class ShopOnWheelsDbContextProviderExtension
    {
        public static void AddShopOnWheelsDbContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ShopOnWheelsDbContext>(provider => provider.UseMySQL(connection));
        }
    }
}
