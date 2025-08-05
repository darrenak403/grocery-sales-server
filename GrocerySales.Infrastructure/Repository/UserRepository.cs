using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Update(User user)
        {
           _context.Users.Update(user); 
        }
    }
}
