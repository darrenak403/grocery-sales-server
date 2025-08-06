using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Dtos.Account;
using GrocerySales.Abstractions.Dtos.Customer;
using GrocerySales.Abstractions.Dtos.User;
using GrocerySales.Abstractions.Entities;

namespace GrocerySales.Abstractions.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfomationResponse>> GetAllUserAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task BanAsync(Guid id);
        Task UnbanAsync(Guid id);
        Task <AccountResponse?> AddUserAsync(AccountRequest request);
    }
}
