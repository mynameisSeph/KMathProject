using KM.BackOffice.Application.ViewModels;
using KM.BackOffice.Core.Models;
using KM.BackOffice.Core.Utilities;
using KM.BackOffice.Database.Context;
using KM.BackOffice.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.BackOffice.Application.Repositories
{
    public class UserRepository
    {
        private readonly KDBContext kDBContext;

        public UserRepository(KDBContext KDBContext)
        {
            this.kDBContext = KDBContext;
        }

        public async Task<List<UserViewModel>> getAllUsersAsync()
        {
            try
            {
                var users = await kDBContext.Users.Where(s => s.IsDeleted == false).Select(s => new UserViewModel
                {
                    Id = s.Id,
                    Username = s.Username,
                    FirstName = !string.IsNullOrWhiteSpace(s.FirstName) ? s.FirstName : "",
                    LastName = !string.IsNullOrWhiteSpace(s.LastName) ? s.LastName : "",
                    IsActive = s.IsActive ?? false,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt,
                }).OrderByDescending(s => s.CreatedAt).ToListAsync();

                return users;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserModel> getUsersByIdAsync(int userId)
        {
            try
            {
                var users = await kDBContext.Users.Where(s => s.Id == userId && s.IsDeleted == false).Select(s => new UserModel
                {
                    Username = s.Username,
                    FirstName = !string.IsNullOrWhiteSpace(s.FirstName) ? s.FirstName : "",
                    LastName = !string.IsNullOrWhiteSpace(s.LastName) ? s.LastName : "",
                    IsActive = s.IsActive ?? false,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt,
                }).FirstOrDefaultAsync();

                return users ?? new UserModel();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> insertUsersAsync(UserModel userReq)
        {
            try
            {
                var salt = new HashPasswordWithSalt().GetSaltKey(8);
                var hashPwd = HashPasswordWithSalt.HashPwdWithSalt(userReq.Password, salt);

                var user = new Database.Entities.User();
                user.Username = userReq.Username ?? "";
                user.Salt = salt;
                user.HashPassword = hashPwd;
                user.FirstName = userReq.FirstName ?? "";
                user.LastName = userReq.LastName ?? "";
                user.CreatedAt = DateTime.UtcNow;
                user.IsActive = userReq.IsActive;
                user.UpdatedAt = DateTime.UtcNow;

                kDBContext.Add(user);
                await kDBContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> updateUsersAsync(UserModel userReq)
        {
            if (userReq.Id < 1)
                return false;

            var existingUser = await kDBContext.Users.FindAsync(userReq.Id);

            if (existingUser == null)
                return false;

            var salt = new HashPasswordWithSalt().GetSaltKey(8);
            var hashPwd = HashPasswordWithSalt.HashPwdWithSalt(userReq.Password, salt);

            existingUser.Username = userReq.Username ?? "";
            existingUser.Salt = salt;
            existingUser.HashPassword = hashPwd;
            existingUser.FirstName = !string.IsNullOrWhiteSpace(userReq.FirstName) ? userReq.FirstName : "";
            existingUser.LastName = !string.IsNullOrWhiteSpace(userReq.LastName) ? userReq.LastName : "";
            existingUser.CreatedAt = userReq.CreatedAt;
            existingUser.UpdatedAt = DateTime.Now;
            existingUser.IsActive = userReq.IsActive;

            await kDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteUserAsync(int userId)
        {
            try
            {
                if (userId < 1)
                    return false;

                var existingUser = await kDBContext.Users.FindAsync(userId);

                if (existingUser == null)
                    return false;

                existingUser.IsDeleted = true;
                await kDBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
