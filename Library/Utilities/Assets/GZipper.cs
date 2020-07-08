using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Library.Utilities.Assets
{
    public static class GZipper
    {
        public static string Zip(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {
                msi.CopyTo(gs);
            }

            return Convert.ToBase64String(mso.ToArray());
        }

        public static string Unzip(string base64EncodedInput)
        {
            var bytes = Convert.FromBase64String(base64EncodedInput);

            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(msi, CompressionMode.Decompress))
            {
                gs.CopyTo(mso);
            }

            return Encoding.UTF8.GetString(mso.ToArray());
        }
    }
}