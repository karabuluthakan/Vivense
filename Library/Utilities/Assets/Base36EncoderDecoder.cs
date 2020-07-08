using System;

namespace Library.Utilities.Assets
{
    /// <summary>
    /// 
    /// </summary>
    public class Base36EncoderDecoder
    {
        protected const string INDEX = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Encode(int value, int format = 0)
        {
            var BASE = INDEX.Length;
            var result = string.Empty;
            while (value != 0)
            {
                var idx = value % BASE;
                value /= BASE;
                result = INDEX[idx] + result;
            }

            if (format != 0)
            {
                for (var i = result.Length; i < format; ++i)
                {
                    result = '0' + result;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Decode(string value)
        {
            var BASE = INDEX.Length;
            var result = 0;
            var len = value.Length;
            for (var idx = 0; idx < len; ++idx)
            {
                var val = value[len - idx - 1];

                result += GetIndex(val) * Convert.ToInt32(Math.Pow(BASE, idx));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static int GetIndex(char val)
        {
            var BASE = INDEX.Length;
            for (var i = 0; i < BASE; ++i)
            {
                if (INDEX[i] == val)
                    return i;
            }

            return -1;
        }
    }
}