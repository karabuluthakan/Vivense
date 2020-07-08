using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Utilities.Assets
{
    /// <summary>
    ///     Password Generator
    /// </summary>
    public static class RandomGenerator
    {
        /// <summary>
        ///     Generate random password with default value of 8
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password : ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_- 
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!*";
            var random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            var chars = new char[length];
            for (var idx = 0; idx < length; idx++)
            {
                chars[idx] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }

        /// <summary>
        ///     Creates random filename
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomFileName()
        {
            const string parity = "abcdef0123456789";
            var strGuid = Guid.NewGuid().ToString("N");
            var nIdx = strGuid[6] % 16;
            var nLen = strGuid.Length;
            return strGuid.Substring(0, nLen - 1) + parity[nIdx];
        }

        /// <summary>
        ///     Generate HashedPassword with Salt
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPasswordWithSalt(string password)
        {
            var salt = CryptoHandler.GenerateSalt();
            var hashedPassword = CryptoHandler.GenerateHash(password, salt);

            return hashedPassword + ":" + salt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="count"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<string> CreateRandomCouponCode(string prefix, int count, int size = 7)
        {
            // Create a string of characters, numbers, special characters that allowed in the coupon : ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            if (Math.Pow(validChars.Count(), size) < count)
            {
                return null;
            }

            var codes = new HashSet<string>();

            var couponPrefix = string.Empty;

            if (null != prefix)
            {
                couponPrefix = prefix.ToUpper();
            }

            while (codes.Count < count)
            {
                var chars = new char[size];

                for (var idx = 0; idx < size; idx++)
                {
                    chars[idx] = validChars[random.Next(0, validChars.Length)];
                }

                var coupon = new string(chars);

                coupon = string.Concat(couponPrefix, coupon);

                codes.Add(coupon);
            }

            return codes.ToList();
        }
    }
}