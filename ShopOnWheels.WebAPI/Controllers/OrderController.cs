﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnWheels.Entities.Models.Order;
using ShopOnWheels.Services.Services.OrderService;
using ShopOnWheels.Services.Stores.OrderStore;

namespace ShopOnWheels.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class OrderController : Controller
    {
        private readonly IOrderStore _orderStore;
        private readonly IOrderService _orderService;

        public OrderController(IOrderStore orderStore, IOrderService orderService)
        {
            _orderStore = orderStore;
            _orderService = orderService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orderStore.GetOrders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _orderStore.GetOrder(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderDTO value)
        {
            return Ok(await _orderStore.AddOrder(value));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _orderStore.DeleteOrder(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]OrderDTO value)
        {
            return Ok(await _orderStore.UpdateOrder(id, value));
        }

        [HttpPost]
        [Route("products/add")]
        public async Task<IActionResult> AddProducts([FromBody]Guid[] productsId, Guid orderId)
        {
            return Ok(await _orderService.AddProducts(productsId, orderId));
        }

        [HttpPost]
        [Route("products/delete")]
        public async Task<IActionResult> DeleteProducts([FromBody]Guid[] productsId, Guid orderId)
        {
            return Ok(await _orderService.DeleteProducts(productsId, orderId));
        }
    }
}