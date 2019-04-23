using AutoMapper;
using ShopOnWheels.Domain;
using ShopOnWheels.Entities.Models.Order;
using ShopOnWheels.Entities.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnWheels.Services.Services.OrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> AddProducts(Guid[] productsId, Guid orderId);
        Task<IEnumerable<OrderDTO>> DeleteProducts(Guid[] productsId, Guid orderId);
    }
}
