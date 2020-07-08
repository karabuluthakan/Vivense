using System.Collections.Generic;
using Library.Models.Enums;
using Library.Models.Lookups;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.ApacheKafka.Events.Product.Infos
{
    /// <summary>
    /// 
    /// </summary>
    public enum AlternativeTypeInfo
    {
        Product,
        Category
    }
    /// <summary>
    /// 
    /// </summary>
    public class ProductTupleInfo
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MultiLanguage Title { get; set; }
        public ProductType Type { get; set; }
        public string Upc { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CategoryTupleInfo
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MultiLanguage Title { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class AlternativeComponentInfo
    {
        public AlternativeTypeInfo AlternativeType { get; set; }
        public List<ProductTupleInfo> Products { get; set; } = new List<ProductTupleInfo>();
        public List<CategoryTupleInfo> Categories { get; set; } = new List<CategoryTupleInfo>();
    }

    public class ComponentInfo
    {
        public MultiLanguage Title { get; set; } = new MultiLanguage();
        public ProductTupleInfo DefaultProduct { get; set; }
        public bool Optional { get; set; }
        public double MinUnit { get; set; }
        public double DefaultUnit { get; set; }
        public double MaxUnit { get; set; }
        public AlternativeComponentInfo Alternatives { get; set; } = new AlternativeComponentInfo();
    }


}
