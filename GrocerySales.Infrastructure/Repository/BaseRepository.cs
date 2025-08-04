using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.IRepository;
using GrocerySales.Infrastructure.Data;

namespace GrocerySales.Infrastructure.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly GrocerySalesContext _context;
        public BaseRepository(GrocerySalesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }    
}

