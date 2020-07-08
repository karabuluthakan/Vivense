using System.Collections.Generic;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Promotion
{
    public class PromotionCalculateEventInfo
    {
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public List<string> CollectionIds { get; set; } = new List<string>();
        public PromotionEventInfoProductType Type { get; set; }
        public PromotionEventInfoQuantityUnit QuantityUnit { get; set; }
        public int MinimumOrderableQuantity { get; set; } = 1;
        public PromotionEventInfoShipment Shipment { get; set; }
        public PromotionEventInfoFulfillmentType FulfillmentType { get; set; }

        public List<PromotionCalculateEventInfoProductSupplier> Suppliers { get; set; } = new List<PromotionCalculateEventInfoProductSupplier>();

    }

    public class PromotionCalculateEventInfoProductSupplier
    {
        public string SupplierId { get; set; }
        public List<PromotionCalculateEventInfoMarketPlacePolicy> marketPlacePolicies { get; set; } = new List<PromotionCalculateEventInfoMarketPlacePolicy>();
    }

    public class PromotionCalculateEventInfoMarketPlacePolicy
    {
        public string MarketPlaceId { get; set; }
        public List<PromotionCustomerGroupCalculateEventInfoVolumePrice> PromotionCustomerGroupPrices { get; set; } = new List<PromotionCustomerGroupCalculateEventInfoVolumePrice>();
        public List<PromotionCalculateEventInfoVolumePrice> Prices { get; set; } = new List<PromotionCalculateEventInfoVolumePrice>();
    }

    public class PromotionCalculateEventInfoVolumePrice
    {
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
    }

    public class PromotionCustomerGroupCalculateEventInfoVolumePrice
    {
        public LookupIdName Promotion { get; set; }
        
        public List<LookupIdName> CustomerGroups { get; set; } = new List<LookupIdName>();
        
        public List<PromotionCalculateEventInfoVolumePrice> Prices = new List<PromotionCalculateEventInfoVolumePrice>();
    }
}
