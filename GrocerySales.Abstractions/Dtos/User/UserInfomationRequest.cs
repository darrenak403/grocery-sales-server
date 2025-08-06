using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrocerySales.Abstractions.Dtos.User
{
    public class UserInfomationRequest
    {
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;

        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int RoleId { get; set; }
    }
}
