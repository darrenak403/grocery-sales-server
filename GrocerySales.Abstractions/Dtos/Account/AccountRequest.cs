using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrocerySales.Abstractions.Dtos.Account
{
    public class AccountRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = null!;
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string RoleName { get; set; } = default!;

    }
}
