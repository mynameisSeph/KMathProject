using KM.BackOffice.Core.Utilities;
using KM.BackOffice.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.BackOffice.Application.Repositories
{
    public class AccountRepository
    {
        private readonly KDBContext _kDBContext;

        public AccountRepository(KDBContext KDBContext)
        {
            _kDBContext = KDBContext;
        }

        public async Task<bool> SignInAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            var user = await _kDBContext.Users.Where(s => s.Username == username).FirstOrDefaultAsync();

            if (user == null)
                return false;

            //bool loginPass = HashPasswordWithSalt.VerifyPassword(password, user.HashPassword, user.Salt);

            return true;

        }
    }
}
