using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Dtos.User;

namespace GrocerySales.Abstractions.IServices
{
    public interface IUserProfileService
    {
        Task<UserProfileRespone?> GetUserProfileByIdAsync(Guid userId);
        Task<UserProfileRespone?> UpdateUserProfileAsync(Guid userId, UserProfileRespone userProfile);
        Task<string?> UpdateAvatarAsync(Guid userId, string avatarUrl);
    }
}
