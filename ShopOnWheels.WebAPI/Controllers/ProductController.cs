using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnWheels.Entities.Models.Product;
using ShopOnWheels.Services.Services.FileService;
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
        private readonly IFileService _fileService;

        public ProductController(IProductStore productStore, IProductService productService, IFileService fileService)
        {
            _productStore = productStore;
            _productService = productService;
            _fileService = fileService;
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
        public async Task<IActionResult> Post([FromForm]ProductDTO value, [FromForm] IFormFile image)
        {
            if (image != null)
            {
                value.Image = _fileService.GetUniqueFileName(image.FileName);
            }

            var item = await _productStore.AddProduct(value);

            if (value.Image != null)
            {
                _fileService.SaveFile(image, value.Image);
            }

            return Ok(item);
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
        public async Task<IActionResult> Search([FromQuery] ProductSearchDTO parameters)
        {
            return Ok(await _productService.SearchProducts(parameters));
        }
    }
}
