using System;
using System.Security.Cryptography;
using System.Text;

namespace Library.Utilities.Assets
{
    public static class CryptoHandler
    {
        /// <summary>
        ///     Generates random SALT with provided size
        /// </summary>
        /// <param name="size">Size of generated random numbers</param>
        /// <returns>Returns generated random number with given size</returns>
        public static string GenerateSalt(int size = 32)
        {
            return GenerateRandomNumber(size);
        }

        /// <summary>
        ///     Generates random RefreshToken with default size of 32
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GenerateRefreshToken(int size = 32)
        {
            return GenerateRandomNumber(size);
        }

        /// <summary>
        ///     Generates random number
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string GenerateRandomNumber(int size = 128)
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[size];
            rng.GetBytes(buffer);

            return BitConverter.ToString(buffer).Replace("-", "");
        }

        /// <summary>
        ///     Generates MD5 HASH with provided salt
        /// </summary>
        /// <param name="toBeSalted">Plain input text</param>
        /// <param name="salt">Generated salt text</param>
        /// <returns>Returns hashed string</returns>
        public static string GenerateHash(string toBeSalted, string salt)
        {
            var md5 = new MD5CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(toBeSalted + salt);
            var hashed = md5.ComputeHash(bytes);

            return BitConverter.ToString(hashed).Replace("-", "");
        }

        /// <summary>
        ///     Compares given inputs
        /// </summary>
        /// <param name="hashedInput"></param>
        /// <param name="plainInput"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static bool AreEqual(string hashedInput, string plainInput, string salt)
        {
            var newHash = GenerateHash(plainInput, salt);
            return newHash.ToLower().Equals(hashedInput.ToLower());
        }

        /// <summary>
        ///     base64.urlsafe_b64encode(s)¶
        ///     Encode string s using the URL- and filesystem-safe alphabet, 
        ///     which substitutes - instead of + and _ instead of / in the standard Base64 alphabet.
        ///     The result can still contain =.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="toBeSigned"></param>
        /// <returns></returns>
        public static string HMAC(string key, string toBeSigned)
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var hmacSha1 = new HMACSHA1(keyBytes);
            var hashed = hmacSha1.ComputeHash(Encoding.UTF8.GetBytes(toBeSigned));

            var base64 = Convert.ToBase64String(hashed);

            var plusRemoved = base64.Replace('+', '-');
            return plusRemoved.Replace('/', '_');
        }
    }
}