using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Library.Utilities.Uploads
{
    public interface IFileUploader
    {
        string GenerateNewFilePath(string originalFilename, string contentType, out string thumbFilename, out string profileFilename,
            string transactionId = null);
        Task<IEnumerable<UploadedFileInfo>> UploadFiles(HttpRequest request, string transactionId = null);
        Task<Stream> GetFileObjectByKey(string key, bool tempBucket = false);
        Task<UploadedFileInfo> CopyTempToAnotherFolder(UploadedFileInfo tempFile);
    }
}