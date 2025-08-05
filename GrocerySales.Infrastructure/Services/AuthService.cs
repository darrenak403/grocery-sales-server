using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Dtos.Authentication;
using GrocerySales.Abstractions.Entities;
using GrocerySales.Abstractions.IRepository;
using GrocerySales.Abstractions.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GrocerySales.Infrastructure.Services
{
    public class AuthService(IBaseRepository baseRepository, IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration) : IAuthService
    {
        public async Task<TokenResponse?> LoginAsync(UserLoginRequest request)
        {
            var user = await userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                return null; // User not found
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await CreateTokenResponse(user);
        }
        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await userRepository.GetByIdAsync(userId);

            if(user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return null; // Invalid user or refresh token
            }
            return user;
        }

        private async Task<TokenResponse?> CreateTokenResponse(User user)
        {
            var response = new TokenResponse
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
            return response;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role!.Name ?? string.Empty),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            userRepository.Update(user);
            await baseRepository.SaveChangesAsync();
            return refreshToken;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }



        public async Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request)
        {
            User? user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
                return null;
            return await CreateTokenResponse(user);
        }

        public async Task<bool> CheckLoginAsync(UserLoginRequest request)
        {
            var user = await userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return false;
            }
            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return false;
            }
            return true; 
        }

        public async Task<bool> RegisterAsync(UserRegisterRequest request)
        {
            var existEmail = await userRepository.GetByEmailAsync(request.Email);
            var existPhoneNumber = await userRepository.GetByPhoneNumberAsync(request.PhoneNumber); 
            if (existEmail != null || existPhoneNumber != null)
            {
                return false; 
            }

            var role = await roleRepository.GetByNameAsync("Customer"); 
            if(role == null)
            {
                return false;
            }

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                RoleId = role.RoleId,
                PasswordHash = new PasswordHasher<User>().HashPassword(null!, request.Password)
            };
            userRepository.Add(user);
            await baseRepository.SaveChangesAsync();
            return true;

        }
    }
}