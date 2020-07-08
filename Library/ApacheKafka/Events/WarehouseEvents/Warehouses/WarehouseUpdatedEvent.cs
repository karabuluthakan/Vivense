using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.WarehouseEvents.Warehouses
{
    public class WarehouseUpdatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.UpdateWarehouse;

        public bool ConsumeSynchronously => false;

        public WarehouseEventModel Warehouse { get; set; }
    }
}