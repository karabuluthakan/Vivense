using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Library.Utilities.AppSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Library.Utilities.Uploads.Aws
{
    public class AwsS3Manager : IFileUploader
    {
        private readonly AmazonS3Settings amazonS3Settings;

        public AwsS3Manager(IOptions<AmazonS3Settings> options)
        {
            amazonS3Settings = options.Value;
        }
        
        public string GenerateNewFilePath(string originalFilename, string contentType, out string thumbFilename, out string profileFilename,
            string transactionId = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UploadedFileInfo>> UploadFiles(HttpRequest request, string transactionId = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<Stream> GetFileObjectByKey(string key, bool tempBucket = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<UploadedFileInfo> CopyTempToAnotherFolder(UploadedFileInfo tempFile)
        {
            throw new System.NotImplementedException();
        }
    }
}