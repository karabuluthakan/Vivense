using Library.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.CrossCuttingConcerns.Authorization.Models
{
    public class UserEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public EntityType Type { get; set; }
    }
}