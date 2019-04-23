using ShopOnWheels.Entities.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnWheels.Services.Stores.OrderStore
{
    public interface IOrderStore
    {
        Task<IEnumerable<OrderDTO>> GetOrders();
        Task<OrderDTO> AddOrder(OrderDTO order);
        Task<OrderDTO> UpdateOrder(Guid id, OrderDTO modelDto);
        Task<OrderDTO> DeleteOrder(Guid id);
        Task<OrderDTO> GetOrder(Guid Id);
    }
}
