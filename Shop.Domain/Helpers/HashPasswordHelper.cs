using System;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Domain.Helpers
{
    public class HashPasswordHelper
    {
        public static string HashPassowrd(string password)
        {
            using(var sha256 = SHA256.Create())  
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Substring(0, 15);
            }  
        }
    }
}