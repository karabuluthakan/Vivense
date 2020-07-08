using System;

namespace Library.Utilities.Uploads
{
    public class UploadedFileInfo
    {
        public string OriginalFileName { get; set; }
        public string ContentType { get; set; }
        public string NewFileName { get; set; }
        public string SecurePath { get; set; }
        public string SecureThumbPath { get; set; }
        public string SecureProfilePath { get; set; }
        public DateTime UploadTimeUtc { get; set; } = DateTime.UtcNow;
    }
}