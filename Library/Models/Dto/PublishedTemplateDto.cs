using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Models.Dto
{
    public class PublishedTemplateDto : IDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public string Id { get; set; }

        public string Name { get; set; }

        public List<PublishedTemplatePage> Pages { get; set; } = new List<PublishedTemplatePage>();

        /// <summary>
        ///     Constructor
        /// </summary>
        protected PublishedTemplateDto()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

    }
}