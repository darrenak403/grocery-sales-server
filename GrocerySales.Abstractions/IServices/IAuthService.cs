using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Dtos.Authentication;
using GrocerySales.Abstractions.Entities;

namespace GrocerySales.Abstractions.IServices
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(UserLoginRequest request);
    }
}
