using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShopOnWheels.Domain.Models.User;
using ShopOnWheels.Entities.Models.User;
using ShopOnWheels.Services.Services.TokenService;
using ShopOnWheels.Domain.Identity;

namespace ShopOnWheels.Services.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;


        public AuthService(
            IMapper mapper, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ITokenService tokenService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }


        public async Task<bool> Register(UserRegisterDTO model)
        {
            User user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, Roles.User);

                await _signInManager.SignInAsync(user, false);

                return true;
            }

            return false;
        }

        public async Task<string> Login(UserLoginDTO model)
        {

            var identity = await GetClaimsIdentity(model.Email, model.Password);

            if (identity == null)
            {
                return null;
            }


            return await _tokenService.GenerateEncodedToken(model.Email, identity);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                // get the user to verifty
                var userToVerify = await _userManager.FindByNameAsync(email);

                var userRoles = await _userManager.GetRolesAsync(userToVerify);

                if (userToVerify != null)
                {
                    // check the credentials  
                    if (await _userManager.CheckPasswordAsync(userToVerify, password))
                    {
                        return await Task.FromResult(_tokenService.GenerateClaimsIdentity(email, userToVerify.Id, userRoles[0]));
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
