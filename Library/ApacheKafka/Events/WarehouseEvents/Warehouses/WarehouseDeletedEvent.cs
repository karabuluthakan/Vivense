using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.WarehouseEvents.Warehouses
{
    public class WarehouseDeletedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.DeleteWarehouse;

        public bool ConsumeSynchronously => false;

        public string WarehouseId { get; set; }
    }
}