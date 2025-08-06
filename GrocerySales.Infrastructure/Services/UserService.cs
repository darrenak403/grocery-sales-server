using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using GrocerySales.Abstractions.Dtos.Account;
using GrocerySales.Abstractions.Dtos.Customer;
using GrocerySales.Abstractions.Dtos.User;
using GrocerySales.Abstractions.Entities;
using GrocerySales.Abstractions.IRepository;
using GrocerySales.Abstractions.IServices;
using GrocerySales.Infrastructure.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace GrocerySales.Infrastructure.Services
{
    public class UserService(IUserRepository userRepository, IBaseRepository baseRepository, IRoleRepository roleRepository) : IUserService
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

        public async Task<AccountResponse?> AddUserAsync(AccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Account request cannot be null.");
            }

            var existingUserEmail = await userRepository.GetByEmailAsync(request.Email);
            if (existingUserEmail != null)
            {
                throw new InvalidOperationException($"User with email {request.Email} already exists.");
            }

            var existingUserPhone = await userRepository.GetByPhoneNumberAsync(request.PhoneNumber);
            if (existingUserPhone != null)
            {
                throw new InvalidOperationException($"User with phone number {request.PhoneNumber} already exists.");
            }

            var role = await roleRepository.GetByNameAsync(request.RoleName);
            if (role == null)
            {
                throw new InvalidOperationException($"Role '{request.RoleName}' does not exist.");
            }

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = new PasswordHasher<User>().HashPassword(null!, request.Password),
                RoleId = role.RoleId,
                CreatedAt = DateTime.UtcNow,
                Status = true
            };

            userRepository.Add(user);
            await baseRepository.SaveChangesAsync();

            return new AccountResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleName = request.RoleName
            };
        }

    }
}
