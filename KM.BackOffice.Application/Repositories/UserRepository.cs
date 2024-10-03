using KM.BackOffice.Application.ViewModels;
using KM.BackOffice.Core.Models;
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
                    Username = s.Username,
                    Password = s.Password,
                    FirstName = !string.IsNullOrWhiteSpace(s.FirstName) ? s.FirstName : "",
                    LastName = !string.IsNullOrWhiteSpace(s.LastName) ? s.LastName : "",
                    Email = s.Email,
                    IsActive = s.IsActive,
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

        public async Task<UserReq> getUsersByIdAsync(int userId)
        {
            try
            {
                var users = await kDBContext.Users.Where(s => s.Id == userId && s.IsDeleted == false).Select(s => new UserReq
                {
                    Username = s.Username,
                    Password = s.Password,
                    FirstName = !string.IsNullOrWhiteSpace(s.FirstName) ? s.FirstName : "",
                    LastName = !string.IsNullOrWhiteSpace(s.LastName) ? s.LastName : "",
                    Email = s.Email,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt,
                }).FirstOrDefaultAsync();

                return users ?? new UserReq();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> insertUsersAsync(UserReq userReq)
        {
            try
            {
                if (userReq == null)
                    return false;

                var user = new User();
                user.Username = userReq.Username;
                user.Email = userReq.Email;
                user.Password = userReq.Password;
                user.FirstName = !string.IsNullOrWhiteSpace(userReq.FirstName) ? user.FirstName : "";
                user.LastName = !string.IsNullOrWhiteSpace(userReq.LastName) ? user.LastName : "";
                user.CreatedAt = DateTime.UtcNow;
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

        public async Task<bool> updateUsersAsync(int userId, UserReq userReq)
        {
            if (userReq == null && userId < 1)
                return false;

            var indexUser = await kDBContext.Users.FindAsync(userId);

            if (indexUser == null)
                return false;

            indexUser.Username = userReq.Username;
            indexUser.Password = userReq.Password;
            indexUser.Email = userReq.Email;
            indexUser.FirstName = !string.IsNullOrWhiteSpace(userReq.FirstName) ? userReq.FirstName : "";
            indexUser.LastName = !string.IsNullOrWhiteSpace(userReq.LastName) ? userReq.LastName : "";
            indexUser.CreatedAt = userReq.CreatedAt;
            indexUser.UpdatedAt = DateTime.UtcNow;
            indexUser.IsActive = userReq.IsActive;

            await kDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteUserById(int userId)
        {
            try
            {
                if (userId < 1)
                    return false;

                var indexUser = await kDBContext.Users.FindAsync(userId);

                if (indexUser == null)
                    return false;

                indexUser.IsDeleted = true;
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
