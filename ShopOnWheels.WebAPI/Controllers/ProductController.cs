using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnWheels.Entities.Models.Product;
using ShopOnWheels.Services.Services.ProductService;
using ShopOnWheels.Services.Stores.ProductStore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOnWheels.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductController : Controller
    {
        private readonly IProductStore _productStore;
        private readonly IProductService _productService;

        public ProductController(IProductStore productStore, IProductService productService)
        {
            _productStore = productStore;
            _productService = productService;
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
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Post([FromBody]ProductDTO value)
        {
            return Ok(await _productStore.AddProduct(value));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProductDTO value)
        {
            return Ok(await _productStore.UpdateProduct(id, value));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _productStore.DeleteProduct(id));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(ProductSearchDTO parameters)
        {
            return Ok(await _productService.SearchProducts(parameters));
        }
    }
}
