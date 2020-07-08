using System.Collections.Generic;
using Library.Models.Lookups;
using Library.Utilities.Uploads;

namespace Library.ApacheKafka.Events.Product.Infos
{
    public class ProductInfo
    {
        public string Id { get; set; }
        public int LegacyId { get; set; }
        public bool Deleted { get; set; }
        public string VariantId { get; set; }
        public int LegacyVariant { get; set; }
        public LookupIdTitle Category { get; set; }
        public List<LookupIdName> Collections { get; set; } = new List<LookupIdName>();
        public List<LookupIdTitle> SecondaryCategories { get; set; } = new List<LookupIdTitle>();
        public string Upc { get; set; }
        public string Vsin { get; set; }
        public ProductTypeInfo Type { get; set; }
        public ProductQuantityUnit QuantityUnit { get; set; }
        public int MinimumOrderableQuantity { get; set; } = 1;
        public bool ReviewEnabled { get; set; }
        public bool ShopTheLook { get; set; }
        public ProductSearchVisibility SearchVisibility { get; set; }
        public MultiLanguage Title { get; set; } = new MultiLanguage();
        public List<string> Tags { get; set; } = new List<string>();
        public List<UploadedFileInfo> Media { get; set; } = new List<UploadedFileInfo>();
        public List<ComponentInfo> Components { get; set; } = new List<ComponentInfo>();
        public List<ProductAttributeInfo> Attributes { get; set; } = new List<ProductAttributeInfo>();
        public int EditVersion { get; set; }
        public List<ProductSupplierInfo> Suppliers { get; set; } = new List<ProductSupplierInfo>();
    }
}