using System.Collections.Generic;
using Library.Models.Enums;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Product.Infos
{
    public class MarketPlaceInfoTuple
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public LookupCodeTitle Country { get; set; }
        public LookupCodeTitle Currency { get; set; }
    }

    public class MarketPlacePolicyInfo
    {
        public string Id { get; set; }

        public MarketPlaceInfoTuple MarketPlace { get; set; }

        public bool Published { get; set; }
        public int ProdDurationCalendarDays { get; set; }
        public InventoryPolicy InventoryPolicy { get; set; }
        public Shipment Shipment { get; set; }
        public string ShipmentNotes { get; set; }
        public FulfillmentType FulfillmentType { get; set; }
        public InstallationType InstallationType { get; set; }
        public string InstallationNotes { get; set; }
        public ReturnPolicy ReturnPolicy { get; set; }
        public bool PriceIncludesTaxAndCommission { get; set; }
        public List<ProductEventPromotionCustomerGroupPrice> PromotionCustomerGroupPrices { get; set; } = new List<ProductEventPromotionCustomerGroupPrice>();
        public List<VolumePrice> Prices { get; set; } = new List<VolumePrice>();
        public double CommissionRate { get; set; }
        public decimal? GeneralMarketPrice { get; set; }
    }
}
