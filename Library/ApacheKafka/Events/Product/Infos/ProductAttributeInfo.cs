using System;
using System.Collections.Generic;
using Library.Models.Helpers;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Product.Infos
{
    public class ProductAttributeInfo
    {
        public string Id { get; set; }
        public bool UseAsVariant { get; set; }
        public AttributeTypeInfo Type { get; set; } = new AttributeTypeInfo();
        public MultiLanguage Title { get; set; } = new MultiLanguage();
        public List<AttributeOptionInfo> Values { get; set; } = new List<AttributeOptionInfo>();
    }

    public enum AttributeTypeInfo
    {
        Text,
        NumberInt,
        NumberDouble,
        Date,
        Boolean,
        Color
    }

    public class AttributeOptionInfo
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
