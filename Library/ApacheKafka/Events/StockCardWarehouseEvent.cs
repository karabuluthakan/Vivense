using System.Collections.Generic;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Events.Product.Infos;
using Library.ApacheKafka.Persistence;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events
{
    public class StockCardWarehouseEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.StockCardWarehouses;
        public bool ConsumeSynchronously { get; } = false;
        public ProductInfo ProductEventInfo { get; set; }
        public List<LookupIdName> Warehouses { get; set; }

    }
}