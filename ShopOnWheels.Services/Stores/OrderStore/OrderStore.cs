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

namespace ShopOnWheels.Services.Stores.OrderStore
{
    public class OrderStore : IOrderStore
    {

        private readonly IMapper _mapper;
        private readonly ShopOnWheelsDbContext _context;

        public OrderStore(IMapper mapper, ShopOnWheelsDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDTO> AddOrder(OrderDTO order)
        {
            var model = _mapper.Map<Order>(order);

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(model);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            return _mapper.Map<List<OrderDTO>>(_context.Orders
                .Include(o => o.ProductList)
                .ThenInclude(pl => pl.Product));

        }

        public async Task<OrderDTO> UpdateOrder(Guid id, OrderDTO modelDto)
        {
            var model = _mapper.Map<Order>(modelDto);
            model.Id = id;

            _context.Orders.Update(model);

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(model);
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
            return _mapper.Map<OrderDTO>(_context.Orders.FirstOrDefault(p => p.Id == Id));
        }
    }
}
