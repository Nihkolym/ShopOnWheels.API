using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopOnWheels.Domain;
using ShopOnWheels.Domain.Models.Product;
using ShopOnWheels.Domain.Models.ProductList;
using ShopOnWheels.Entities.Models.Order;
using ShopOnWheels.Entities.Models.Product;
using System.Linq;

namespace ShopOnWheels.Services.Services.OrderService
{
    public class OrderService : IOrderService
    {

        private readonly IMapper _mapper;
        private readonly ShopOnWheelsDbContext _context;

        public OrderService(IMapper mapper, ShopOnWheelsDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderDTO>> AddProducts(Guid[] productsId, Guid orderId)
        {
            var productList = new List<ProductList>();

            foreach(var id in productsId)
            {
                productList.Add(new ProductList() { Id = id, OrderId = id });
            }

            await _context.ProductLists.AddRangeAsync(productList);

            await _context.SaveChangesAsync();

            return _mapper.Map<List<OrderDTO>>(_context.Orders.ToList());
        }

        public async Task<IEnumerable<OrderDTO>> DeleteProducts(Guid[] productsId, Guid orderId)
        {
            var productList = new List<ProductList>();

            foreach (var id in productsId)
            {
                productList.Add(new ProductList() { Id = id, OrderId = id });
            }

            _context.ProductLists.RemoveRange(productList);

            await _context.SaveChangesAsync();

            return _mapper.Map<List<OrderDTO>>(_context.Orders.ToList());
        }
    }
}
