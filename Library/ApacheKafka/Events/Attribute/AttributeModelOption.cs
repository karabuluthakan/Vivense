using System;
using Library.Models.Helpers;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Attribute
{
    public class AttributeModelOption
    {
        public string Id { get; set; }

        public MultiLanguage Text { get; set; } = new MultiLanguage();
        public int NumberInt { get; set; }
        public double NumberDouble { get; set; }
        public DateTime Date { get; set; }
        public bool Boolean { get; set; }
        public Color Color { get; set; }

    }
}
