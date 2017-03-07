using System;
using System.Security.Cryptography;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Domain.Services
{
    public class PasswordHash
    {
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;
        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        public static string CreateHash(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                    throw new CriptografiaException("A senha não pode estar vazio ou nula.");

                // Generate a random salt
                var csprng = new RNGCryptoServiceProvider();
                var salt = new byte[SALT_BYTE_SIZE];
                csprng.GetBytes(salt);

                // Hash the password and encode the parameters
                var hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
                return PBKDF2_ITERATIONS + ":" +
                       Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("Algum erro ocorreu durante a Criptografia.", ex);
            }
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                    throw new CriptografiaException("A senha não pode estar vazio ou nula.");
                if (string.IsNullOrEmpty(correctHash))
                    throw new CriptografiaException("A senhaCriptografada não pode estar vazio ou nula.");

                // Extract the parameters from the hash
                char[] delimiter = {':'};
                var split = correctHash.Split(delimiter);
                var iterations = Int32.Parse(split[ITERATION_INDEX]);
                var salt = Convert.FromBase64String(split[SALT_INDEX]);
                var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

                var testHash = PBKDF2(password, salt, iterations, hash.Length);
                return SlowEquals(hash, testHash);
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("Algum erro ocorreu durante a Criptografia.", ex);
            }
        }

        /// <summary>
        ///     Compares two byte arrays in length-constant time. This comparison
        ///     method is used so that password hashes cannot be extracted from
        ///     on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint) a.Length ^ (uint) b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint) (a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        ///     Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}