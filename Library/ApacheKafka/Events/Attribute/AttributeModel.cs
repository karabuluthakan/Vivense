using System.Collections.Generic;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Attribute
{
    public class AttributeModel
    {
        public string Id { get; set; }
        public MultiLanguage Title { get; set; } = new MultiLanguage();
        public List<LookupIdTitle> Categories { get; set; } = new List<LookupIdTitle>();
        public List<string> Tags { get; set; } = new List<string>();
        public bool Optional { get; set; } = true;
        public bool MultiValue { get; set; } = false;
        public bool AddOption { get; set; } = false;
        public bool UseAsSearchItem { get; set; } = false;
        public AttributeModelType Type { get; set; }
        public List<AttributeModelOption> Options { get; set; } = new List<AttributeModelOption>();
    }
}
