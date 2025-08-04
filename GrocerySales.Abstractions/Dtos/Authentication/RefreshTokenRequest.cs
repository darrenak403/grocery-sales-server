using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrocerySales.Abstractions.Dtos.Authentication
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Refresh token is required.")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
