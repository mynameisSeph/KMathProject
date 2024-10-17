using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KM.BackOffice.Core.Utilities
{
    public class HashPasswordWithSalt
    {

        public static bool VerifyPassword(string pwd, string hashPwdStore, string saltStore)
        {
            var hashRecentPwd = HashPwdWithSalt(pwd, saltStore);

            return hashRecentPwd == hashPwdStore;
        }

        public static string HashPwdWithSalt(string password, string salt)
        {
            string passwordWithSalt = password + salt;

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(passwordWithSalt);
                byte[] hashedBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public string GetSaltKey(int size = 36, string ranStr = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890")
        {
            Random _random = new Random();
            StringBuilder result = new StringBuilder(size);

            for (int i = 0; i < size; i++)
            {
                result.Append(ranStr[_random.Next(ranStr.Length)]);
            }

            return result.ToString();
        }
    }
}
