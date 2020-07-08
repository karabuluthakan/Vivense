using System.Collections.Generic;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Product.Infos
{
    public class ProductEventPromotionCustomerGroupPrice
    {
        public LookupIdName Promotion { get; set; }
        public List<LookupIdName> CustomerGroups { get; set; } = new List<LookupIdName>();
        public List<VolumePrice> Prices { get; set; } = new List<VolumePrice>();
    }
}
