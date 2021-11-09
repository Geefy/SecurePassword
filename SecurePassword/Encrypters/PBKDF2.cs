using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SecurePassword
{
    public class PBKDF2
    {
        /// <summary>
        /// Creates and returns a salt from a byte array of size 32
        /// </summary>
        /// <returns></returns>
        public byte[] CreateSalt()
        {
            
            using(RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[32];
                rng.GetBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// Hashes a password with a provided salt and number of rounds to hash.
        /// </summary>
        /// <param name="passwordToBeHashed"></param>
        /// <param name="salt"></param>
        /// <param name="numberOfRounds"></param>
        /// <returns></returns>
        public byte[] PasswordHasher(string passwordToBeHashed, byte[] salt, int numberOfRounds)
        {
            
            using (Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(passwordToBeHashed), salt, numberOfRounds))
            {
                return rfc2898.GetBytes(32);
            }
        }
    }
}
