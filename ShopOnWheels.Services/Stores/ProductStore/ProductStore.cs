using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopOnWheels.Domain;
using ShopOnWheels.Domain.Models.Order;
using ShopOnWheels.Entities.Models.Product;
using ShopOnWheels.Services.Stores.ProductStore;

namespace ShopOnWheels.Services.Stores.ProductsStore
{
    public class ProductStore : IProductStore
    {
        private readonly IMapper _mapper;
        private readonly ShopOnWheelsDbContext _context;

        public ProductStore(IMapper mapper, ShopOnWheelsDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductDTO> AddProduct(ProductDTO modelDto)
        {
            var model = _mapper.Map<Product>(modelDto);

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
 
            return _mapper.Map<ProductDTO>(model);
        }

        public async Task<IEnumerable<ProductDTO>> AllProducts()
        {
            var models = _context.Products.ToList();
            return _mapper.Map<IEnumerable<ProductDTO>>(models);
        }

        public async Task<ProductDTO> DeleteProduct(Guid Id)
        {
            var model = _context.Products.FirstOrDefault(p => p.Id == Id);
            _context.Products.Remove(model);
            
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(model);
        }

        public async Task<ProductDTO> GetProduct(Guid Id)
        {
            return _mapper.Map<ProductDTO>(_context.Products.FirstOrDefault(p => p.Id == Id));
        }

        public async Task<ProductDTO> UpdateProduct(Guid id, ProductDTO modelDto)
        {
            var model = _mapper.Map<Product>(modelDto);
            model.Id = id;

            _context.Products.Update(model);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(model);
        }


    }
}
