using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace VHSStore.Utility
{
    public class StringHasher
    {
        public string HashedString { get; set; }
        public byte[] Salt { get; set; }

        public StringHasher(string originalString)
        {
            SaltGenerator();
            HashingStringMethod(originalString);
        }

        public StringHasher(string originalString, string salt)
        {
            Salt = Convert.FromBase64String(salt);
            HashingStringMethod(originalString);
        }

        private void SaltGenerator()
        {
            byte[] saltByteArray = new byte[128 / 8];

            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(saltByteArray);
            }

            Salt = saltByteArray;
        }

        private void HashingStringMethod(string originalString)
        {
            HashedString = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: originalString,
                salt: Salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}
