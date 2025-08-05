using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Dtos.Customer;
using GrocerySales.Abstractions.Entities;
using GrocerySales.Abstractions.IRepository;
using GrocerySales.Abstractions.IServices;

namespace GrocerySales.Infrastructure.Services
{
    public class UserService(IUserRepository userRepository, IBaseRepository baseRepository) : IUserService
    {
        public async Task<IEnumerable<UserInfomationResponse>> GetAllUserAsync()
        {
            return await userRepository.GetAllUserAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public async Task BanAsync(Guid id)
        {
            await userRepository.BanAsync(id);
        }

        public async Task UnbanAsync(Guid id)
        {
            await userRepository.UnbanAsync(id);
        }
    }
}
