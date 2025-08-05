using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Entities;

namespace GrocerySales.Abstractions.IRepository
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string roleName);    
    }
}
