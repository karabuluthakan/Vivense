using System.Collections.Generic;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Product.Infos
{
    public class ProductSupplierInfo
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }
        public LookupIdName Supplier { get; set; }
        public string Sku { get; set; }
        public int InventoryThreshold { get; set; }
        public List<ProductPackageInfo> Packages { get; set; } = new List<ProductPackageInfo>();
        public string LotNo { get; set; }
        public List<MarketPlacePolicyInfo> MarketPlacePolicies { get; set; } = new List<MarketPlacePolicyInfo>();
        public MultiLanguage Summary { get; set; } = new MultiLanguage();
    }
}