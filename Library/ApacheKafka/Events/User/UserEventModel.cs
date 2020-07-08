using System.Collections.Generic;
using Library.CrossCuttingConcerns.Authorization.Models;
using Library.Utilities.Uploads;

namespace Library.ApacheKafka.Events.User
{
    public class UserEventModel
    {
        public string Id { get; set; }
        public UserModelType? UserType { get; set; }
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public string Name { get; set; }
        public UploadedFileInfo Avatar { get; set; } = new UploadedFileInfo();
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool EulaAccepted { get; set; }
        public string RoleId { get; set; }
        public List<UserEntity> Entities { get; set; } = new List<UserEntity>();
    }

}
