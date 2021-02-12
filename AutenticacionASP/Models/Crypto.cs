using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace AutenticacionASP.Models
{
    public static class Hash
    {
        private const int PBKDF2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int PBKDF2SubkeyLength = 256 / 8; // 256 bits

		public static string Create(string value, string salt)
		{
			var valueBytes = KeyDerivation.Pbkdf2(
								password: value,
								salt: Encoding.UTF8.GetBytes(salt),
								prf: KeyDerivationPrf.HMACSHA512,
								iterationCount: PBKDF2IterCount,
								numBytesRequested: PBKDF2SubkeyLength);

			return Convert.ToBase64String(valueBytes);
		}

		public static bool Validate(string value, string salt, string hash)
			=> Create(value, salt) == hash;

    }
    public static class Salt
    {
        private const int SaltSize = 128 / 8; // 128 bits
        public static string Create()
        {
            byte[] randomBytes = new byte[SaltSize];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}

