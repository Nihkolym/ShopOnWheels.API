using ShopOnWheels.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnWheels.Services.Services.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateEncodedToken(string email, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id, string role);
    }
}
