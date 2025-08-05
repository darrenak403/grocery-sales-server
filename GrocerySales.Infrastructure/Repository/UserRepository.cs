using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Dtos.Customer;
using GrocerySales.Abstractions.Entities;
using GrocerySales.Abstractions.IRepository;
using GrocerySales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GrocerySales.Infrastructure.Repository
{
    
    public class UserRepository(GrocerySalesContext _context) : IUserRepository
    {
        public void Add(User user)
        {
            _context.Users.Add(user);
        }
        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<IEnumerable<UserInfomationResponse>> GetAllUserAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserInfomationResponse
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    DayOfBirth = u.DayOfBirth,
                    Address = u.Address,
                    Status = u.Status,
                    CreatedAt = u.CreatedAt,
                    RoleName = u.Role.Name
                })
                .ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

        }

        public Task<User?> GetByIdAsync(Guid? userId)
        {
            if (userId == null || userId == Guid.Empty)
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));
            }
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task BanAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("User not found");
            user.Status = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UnbanAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("User not found");
            user.Status = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
