using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Dtos.Customer;
using GrocerySales.Abstractions.Entities;

namespace GrocerySales.Abstractions.IRepository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneNumberAsync(string phoneNumber);
        Task<User?> GetByIdAsync(Guid? userId);
        void Update(User user);
        void Add(User user);
        Task BanAsync(Guid id);
        Task UnbanAsync(Guid id);
        Task<IEnumerable<UserInfomationResponse>> GetAllUserAsync();
    }
}
