using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopOnWheels.Services.Stores.ProductsStore;
using ShopOnWheels.Services.Stores.ProductStore;

namespace ShopOnWheels.WebAPI.Extensions
{
    public static class ServiceComponentsDI
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            services.AddTransient<IProductStore, ProductStore>();
        }
    }
}
