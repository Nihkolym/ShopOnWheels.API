using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopOnWheels.Domain.Models.Product;
using ShopOnWheels.Entities.Models.Product;
using ShopOnWheels.Services.Builders.QueryBuilders.Product;

namespace ShopOnWheels.Services.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductSearchQueryBuilder _queryBuilder;

        public ProductService(IMapper mapper, IProductSearchQueryBuilder queryBuilder)
        {
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        public async Task<IEnumerable<ProductDTO>> SearchProducts(ProductSearchDTO parameters)
        {
            IQueryable<Product> query = await _GetOrderSearchQuery(parameters);

            List<Product> products = query.ToList();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        private async Task<IQueryable<Product>> _GetOrderSearchQuery(ProductSearchDTO parameters)
        {
            IQueryable<Product> query = _queryBuilder.SetBaseProductInfo()
                                                      .SetName(parameters.Name)
                                                      .SetManufacturer(parameters.Manufacturer)
                                                      .SetPrice(parameters.From, parameters.To)
                                                      .SetCategory(parameters.CategoryId)
                                                      .Build();
            return query;
        }
    }
}
