using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShopOnWheels.Domain.Models.User;
using ShopOnWheels.Entities.Models.User;

namespace ShopOnWheels.Services.Services.AuthService
{
    public interface IAuthService
    {
        Task<bool> Register(UserRegisterDTO model);
        Task<string> Login(UserLoginDTO model);
    }
}
