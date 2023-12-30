using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Note.Utilities
{
    internal class PasswordHasher
    {
        public static string Hash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-","").ToLower();
            }
        }

        public static bool Verify(string password, string hashedPassword)
        {
            string enteredHash = Hash(password);
            return string.Equals(enteredHash, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
