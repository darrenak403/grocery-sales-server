using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrocerySales.Abstractions.Dtos.Customer
{
    public class UserInfomationResponse
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public DateOnly? DayOfBirth { get; set; }
        public string? Address { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
