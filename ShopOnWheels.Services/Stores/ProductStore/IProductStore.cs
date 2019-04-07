using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShopOnWheels.Entities.Models.Product;

namespace ShopOnWheels.Services.Stores.ProductStore
{
    public interface IProductStore
    {
        Task<ProductDTO> AddProduct(ProductDTO modelDto);
        Task<ProductDTO> UpdateProduct(Guid id, ProductDTO modelDto);
        Task<ProductDTO> GetProduct(Guid Id);
        Task<IEnumerable<ProductDTO>> AllProducts();
        Task<ProductDTO> DeleteProduct(Guid Id);
    }
}
