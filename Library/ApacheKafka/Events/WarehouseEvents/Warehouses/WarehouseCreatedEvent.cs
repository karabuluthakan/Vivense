using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.WarehouseEvents.Warehouses
{
    public class WarehouseCreatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.CreateNewWarehouse;

        public bool ConsumeSynchronously => false;

        public WarehouseEventModel Warehouse { get; set; }
    }
}