using Library.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Models.Dto
{
    public class PublishedTemplatePage
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }

        public PageName Name { get; set; }

        public TemplateAssets Assets { get; set; } = new TemplateAssets();

        public string Html { get; set; }
 
        public PublishedTemplatePage()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}