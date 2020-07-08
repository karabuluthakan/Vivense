using System.Collections.Generic;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Promotion
{
    public class PromotionQualifierInfo
    {
        public PromotionEventInfoQualifier Qualifiying { get; set; }
        public List<LookupIdName> QualifiedProducts { get; set; } = new List<LookupIdName>();
        public double? Buy { get; set; }
        public List<LookupIdName> DiscountedProducts { get; set; } = new List<LookupIdName>();

    }

}
