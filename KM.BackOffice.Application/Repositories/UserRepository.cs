using KM.BackOffice.Application.ViewModels;
using KM.BackOffice.Database.Context;
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

        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
            try
            {
                var users = await kDBContext.Users.Select(s => new UserViewModel
                {
                    Username = s.Username,
                    Password = s.Password,
                    FirstName = !string.IsNullOrWhiteSpace(s.FirstName) ? s.FirstName : "",
                    LastName = !string.IsNullOrWhiteSpace(s.LastName) ? s.LastName : "",
                    Email = s.Email,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt,
                }).ToListAsync();

                return users;
            }
            catch
            {
                throw;
            }
        }
    }
}
