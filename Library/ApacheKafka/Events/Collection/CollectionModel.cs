using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Collection
{
    public class CollectionModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public MultiLanguage Desc { get; set; } = new MultiLanguage();
        public bool Published { get; set; }
    }
}
