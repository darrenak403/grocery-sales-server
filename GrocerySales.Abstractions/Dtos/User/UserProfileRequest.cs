using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrocerySales.Abstractions.Dtos.User
{
    public class UserProfileRequest
    {
        public string? Username { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string PhoneNumber { get; set; } = default!;

        public string? AvatarUrl { get; set; }

        public DateOnly? DayOfBirth { get; set; }

        public string? Address { get; set; } = default!;
    }
}
