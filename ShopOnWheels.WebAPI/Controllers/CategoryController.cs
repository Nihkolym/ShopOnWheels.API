using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnWheels.Entities.Models.Category;
using ShopOnWheels.Services.Stores.CategoryStore;

namespace ShopOnWheels.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryStore _categoryStore;

        public CategoryController(ICategoryStore categoryStore)
        {
            _categoryStore = categoryStore;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryStore.AllCategories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _categoryStore.GetCategory(id));
        }

        [HttpPost]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Post([FromBody]CategoryDTO value)
        {
            return Ok(await _categoryStore.AddCategory(value));
        }


        [HttpDelete("{id}")]

        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _categoryStore.DeleteCategory(id));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CategoryDTO value)
        {
            return Ok(await _categoryStore.UpdateCategory(id, value));
        }
    }
}