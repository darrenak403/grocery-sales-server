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
    public class RoleRepository(GrocerySalesContext _context) : IRoleRepository
    {
        public async Task<Role?> GetByNameAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Role name cannot be null or empty.", nameof(roleName));
            }
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
