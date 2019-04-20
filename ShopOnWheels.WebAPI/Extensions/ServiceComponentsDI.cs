using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopOnWheels.Services.Services.AuthService;
using ShopOnWheels.Services.Stores.ProductsStore;
using ShopOnWheels.Services.Stores.ProductStore;
using ShopOnWheels.Services.Services.TokenService;

namespace ShopOnWheels.WebAPI.Extensions
{
    public static class ServiceComponentsDI
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IProductStore, ProductStore>();
        }
    }
}
