using ShopOnWheels.Entities.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnWheels.Services.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> SearchProducts(ProductSearchDTO parameters);
    }
}
