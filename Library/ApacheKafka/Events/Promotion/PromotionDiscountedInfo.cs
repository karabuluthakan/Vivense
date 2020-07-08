using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Promotion
{
    public class PromotionDiscountedInfo
    {
        public PromotionEventInfoQualifier Qualifier { get; set; }
        public LookupIdName[] DiscountedIds { get; set; }
    }

}
