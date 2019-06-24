using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShopOnWheels.Domain.Models.Order;
using ShopOnWheels.Entities.Models.Order;
using System.Linq;
using ShopOnWheels.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ShopOnWheels.Services.Services.OrderService;
using ShopOnWheels.Domain.Models.ProductList;

namespace ShopOnWheels.Services.Stores.OrderStore
{
    public class OrderStore : IOrderStore
    {

        private readonly IMapper _mapper;
        private readonly ShopOnWheelsDbContext _context;
        private readonly IOrderService _orderService;

        public OrderStore(IMapper mapper, ShopOnWheelsDbContext context, IOrderService orderService)
        {
            _context = context;
            _mapper = mapper;
            _orderService = orderService;
        }

        public async Task<OrderDTO> AddOrder(OrderDTO order)
        {
            if (order.Frequency != null)
            {
                order.IsActive = true;
            } 

            var model = _mapper.Map<Order>(order);

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            var productList = new List<ProductList>();

            foreach (var id in order.Products.Select(p => p.Id).ToArray())
            {
                productList.Add(new ProductList() { ProductId = id, OrderId = model.Id });
            }

            await _context.ProductLists.AddRangeAsync(productList);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(model);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders(string userId)
        {
            return _mapper.Map<List<OrderDTO>>(_context.Orders
                .Include(o => o.ProductList)
                .ThenInclude(pl => pl.Product)
                .Where(o => o.UserId == userId));

        }

        public async Task<OrderDTO> UpdateOrder(Guid id, OrderDTO modelDto)
        {
            var model = _mapper.Map<Order>(modelDto);
            model.Id = id;

            _context.Orders.Update(model);

            await _context.SaveChangesAsync();

            return await this.GetOrder(id);
        }

        public async Task<OrderDTO> DeleteOrder(Guid Id)
        {
            var model = _context.Orders.FirstOrDefault(p => p.Id == Id);
            _context.Orders.Remove(model);

            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDTO>(model);
        }

        public async Task<OrderDTO> GetOrder(Guid Id)
        {
            return _mapper.Map<OrderDTO>(_context.Orders.Include(o => o.ProductList)
                .ThenInclude(pl => pl.Product).FirstOrDefault(p => p.Id == Id));
        }
    }
}
