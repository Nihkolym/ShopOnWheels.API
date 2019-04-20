using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopOnWheels.Domain.Models.User;
using ShopOnWheels.Entities.Models.User;
using ShopOnWheels.Services.Services.AuthService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOnWheels.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        // POST api/<controller>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO model)
        {
            if (await _authService.Register(model))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/<controller>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDTO model)
        {
            var jwt = await _authService.Login(model);

            if (jwt == null)
            {
                return BadRequest("Invalid username or password.");
            }
            else
            {
                return Ok(jwt);
            }
        }
    }
}
