using System.Collections.Generic;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Vehicle
{
    public class VehicleCreatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.CreateNewVehicle;

        public bool ConsumeSynchronously => false;

        public VehicleEventModel Vehicle { get; set; }

    }

    public class VehicleUpdatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.UpdateVehicle;

        public bool ConsumeSynchronously => false;

        public VehicleEventModel Vehicle { get; set; }
    }

    public class VehicleDeletedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.DeleteVehicle;

        public bool ConsumeSynchronously => false;

        public string VehicleId { get; set; }
    }

    public class VehicleEventModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlateNo { get; set; }

        /// <summary>
        ///  For performer information (id,name,email, phone)
        /// </summary>
        public List<VehicleDriverEventModel> Drivers { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VehicleDriverEventModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }
    }
}
