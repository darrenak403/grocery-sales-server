using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrocerySales.Abstractions.Dtos.User
{
    public class UserProfileRespone
    {
        public Guid UserId { get; set; }

        public string Username { get; set; } = default!;

        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public string? AvatarUrl { get; set; }

        public DateOnly? DayOfBirth { get; set; }

        public string? Address { get; set; }

    }
}
