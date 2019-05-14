using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopOnWheels.Services.Services.AuthService;
using ShopOnWheels.Services.Stores.ProductsStore;
using ShopOnWheels.Services.Stores.ProductStore;
using ShopOnWheels.Services.Services.TokenService;
using ShopOnWheels.Services.Stores.OrderStore;
using ShopOnWheels.Services.Services.OrderService;
using ShopOnWheels.Services.Services.ProductService;
using ShopOnWheels.Services.Builders.QueryBuilders.Product;
using ShopOnWheels.Services.Stores.CategoryStore;
using ShopOnWheels.Services.Services.FileService;

namespace ShopOnWheels.WebAPI.Extensions
{
    public static class ServiceComponentsDI
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IProductStore, ProductStore>();
            services.AddTransient<IOrderStore, OrderStore>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryStore, CategoryStore>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IProductSearchQueryBuilder, ProductSearchQueryBuilder>();
        }
    }
}
