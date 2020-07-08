using System.Collections.Generic;

namespace Library.ApacheKafka.Events.Promotion
{
    public class PromotionEventInfo
    {
        public string MarketPlaceId { get; set; }     
        public List<PromotionQualifierInfo>  DiscountProducts { get; set; }
        public List<PromotionDiscountedInfo> BonusProducts { get; set; }
    }

    public enum PromotionEventInfoQualifier
    {
        Product,
        Category,
        Supplier,
        Collection
    }

    public enum PromotionEventInfoProductType
    {
        Piece,
        Simple,
        Group,
        Bundle,
        Composite,
        Virtual
    }
    public enum PromotionEventInfoQuantityUnit
    {
        Count,
        Area,
        Volume
    }
    public enum PromotionEventInfoShipment
    {
        Cargo,
        Logistics
    }
    public enum PromotionEventInfoFulfillmentType
    {
        Self,
        Dropshipping,
        ThirdParty
    }
    public enum PromotionEventInfoPriceModel
    {
        Variable,
        Fixed,
    }

}
