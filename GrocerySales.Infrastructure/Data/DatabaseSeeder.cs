using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrocerySales.Abstractions.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GrocerySales.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        private readonly GrocerySalesContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(
            GrocerySalesContext context,
            IConfiguration configuration,
            ILogger<DatabaseSeeder> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                if (await _context.Database.CanConnectAsync())
                {
                    await _context.Database.MigrateAsync();
                    _logger.LogInformation("Database migration completed successfully");
                }

                await SeedRolesAsync();
                await SeedDefaultManagerAsync();

                _logger.LogInformation("Database seeding completed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during database seeding");
                throw;
            }
        }

        private async Task SeedRolesAsync()
        {
            if (await _context.Roles.AnyAsync())
            {
                _logger.LogInformation("Roles already exist, skipping role seeding");
                return;
            }

            var roles = new[]
            {
                new Role { RoleId = 1, Name = "Manager" },
                new Role { RoleId = 2, Name = "Cashier" },
                new Role { RoleId = 3, Name = "Customer" }
            };

            _context.Roles.AddRange(roles);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Roles seeded successfully");
        }

        private async Task SeedDefaultManagerAsync()
        {
            if (await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Users already exist, skipping manager seeding");
                return;
            }

            var adminSection = _configuration.GetSection("DefaultManager");
            var phoneNumber = adminSection["PhoneNumber"];
            var password = adminSection["Password"];
            var email = adminSection["Email"];
            var username = adminSection["Username"];

            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("Default manager configuration is missing");
            }

            var hasher = new PasswordHasher<User>();
            var manager = new User
            {
                UserId = Guid.NewGuid(),
                Username = username,
                Email = email,
                PhoneNumber = phoneNumber,
                FullName = "Default Manager",
                PasswordHash = hasher.HashPassword(null!, password),
                RoleId = 1,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(manager);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Default manager user created successfully");
        }
    }
}
