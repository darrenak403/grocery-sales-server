using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Dtos.User;
using GrocerySales.Abstractions.IRepository;
using GrocerySales.Abstractions.IServices;

namespace GrocerySales.Infrastructure.Services
{
    public class UserProfileService(IBaseRepository baseRepository, IUserRepository userRepository) : IUserProfileService
    {
        public async Task<UserProfileRespone?> GetUserProfileByIdAsync(Guid userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var response =  new UserProfileRespone
            {
                UserId = user.UserId,
                FullName = user.FullName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                Address = user.Address
            };

            return response;
        }

        public async Task<string?> UpdateAvatarAsync(Guid userId, string avatarUrl)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            user.AvatarUrl = avatarUrl;
            userRepository.Update(user);
            await baseRepository.SaveChangesAsync();

            return user.AvatarUrl;
        }

        public async Task<UserProfileRespone?> UpdateUserProfileAsync(Guid userId, UserProfileRespone userProfile)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            user.UserId = userId;
            user.FullName = userProfile.FullName;
            user.Email = userProfile.Email;
            user.PhoneNumber = userProfile.PhoneNumber;
            user.Address = userProfile.Address;
            user.AvatarUrl = userProfile.AvatarUrl;
            user.DayOfBirth = userProfile.DayOfBirth;
            user.Address = userProfile.Address;
            
            userRepository.Update(user);
            await baseRepository.SaveChangesAsync();

            var response = new UserProfileRespone
            {
                UserId = user.UserId,
                FullName = user.FullName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                Address = user.Address,
                DayOfBirth = user.DayOfBirth
            };

            return response;
        }
    }
}
