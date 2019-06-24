using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnWheels.Services.Stores.BoxStore;

namespace ShopOnWheels.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BoxController : ControllerBase
    {
        private readonly IBoxStore _boxStore;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BoxController(IBoxStore boxStore, IHttpContextAccessor httpContextAccessor)
        {
            _boxStore = boxStore;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_boxStore.GetBox(id));
        }
        [HttpGet("orders/{id}")]
        public IActionResult GetAll(Guid id)
        {
            return Ok(_boxStore.GetBoxesByOrder(id));
        }
    }
}