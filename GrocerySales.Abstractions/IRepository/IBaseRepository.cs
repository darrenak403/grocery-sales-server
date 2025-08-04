using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrocerySales.Abstractions.IRepository
{
    public interface IBaseRepository
    {
        Task SaveChangesAsync();
    }
}
