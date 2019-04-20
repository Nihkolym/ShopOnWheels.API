using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnWheels.Entities.Models.Product;
using ShopOnWheels.Services.Stores.ProductStore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOnWheels.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "ApiAdmin", AuthenticationSchemes = "Bearer")]
    public class ProductController : Controller
    {
        private readonly IProductStore _productStore;

        public ProductController(IProductStore productStore)
        {
            this._productStore = productStore;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productStore.AllProducts());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _productStore.GetProduct(id));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductDTO value)
        {
            return Ok(await _productStore.AddProduct(value));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProductDTO value)
        {
            return Ok(await _productStore.UpdateProduct(id, value));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _productStore.DeleteProduct(id));
        }
    }
}
