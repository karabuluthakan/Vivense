using System.Collections.Generic;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;
using Library.CrossCuttingConcerns.Authorization.Models;
using Library.Utilities.Uploads;

namespace Library.ApacheKafka.Events.Product.Events
{
    public class ProductFilesUploadEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.ProductFilesUploaded;

        public bool ConsumeSynchronously => true;

        public string TransactionId { get; set; }
        public List<UploadedFileInfo> UploadedFiles { get; set; }
        public UserInfo UserInfo { get; set; }
    }

    public class UserInfo
    {
        public string UserId { get; set; }
        public List<UserEntity> Entities { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
