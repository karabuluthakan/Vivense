namespace Library.Utilities.Assets
{
    public class SecurePassword
    {
        public string Plain { get; set; }
        public string Hashed { get; set; }
        public string Salt { get; set; }

        /// <summary>
        ///     Constructor    
        /// </summary>
        public SecurePassword(string password = null)
        {
            Plain = string.IsNullOrEmpty(password) ? RandomGenerator.CreateRandomPassword() : password;
            GenerateKeys();
        }

        /// <summary>
        /// 
        /// </summary>
        private void GenerateKeys()
        {
            Salt = CryptoHandler.GenerateSalt();
            Hashed = CryptoHandler.GenerateHash(Plain, Salt) + ":" + Salt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetHashedPassword(string password)
        {
            var salt = CryptoHandler.GenerateSalt();
            return CryptoHandler.GenerateHash(password, salt) + ":" + salt;
        }
    }
}