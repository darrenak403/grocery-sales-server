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
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
           _context.Users.Update(user); 
        }
    }
}
