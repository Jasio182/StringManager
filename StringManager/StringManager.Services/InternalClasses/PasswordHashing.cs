using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace StringManager.Services.InternalClasses
{
    public static class PasswordHashing
    {
        private static byte[] salt = Encoding.ASCII.GetBytes("Y204eGZFekFaY3RTODdNN1Y0TE5SQT09");
        
        public static string HashPassword(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
