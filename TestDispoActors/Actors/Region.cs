using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using TestDispoActors.Actors.Messages;

namespace TestDispoActors.Actors
{
    public class Region : UntypedActor
    {
        protected int RegionId { get; }
        private Dictionary<int, IActorRef> propertyIdToActor = new Dictionary<int, IActorRef>();
        private Dictionary<IActorRef, int> actorToPropertyId = new Dictionary<IActorRef, int>();
        private long nextCollectionId = 0L;
        protected ILoggingAdapter Log { get; } = Context.GetLogger();

        public Region(int regionId)
        {
            RegionId = regionId;
        }

        public static Props Props(int regionId) => Akka.Actor.Props.Create(() => new Region(regionId));

        protected override void PreStart() => Log.Info($"Region {RegionId} started");
        protected override void PostStop() => Log.Info($"Region {RegionId} stopped");

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case RequestAddProperty addMsg when addMsg.RegionId.Equals(RegionId):
                    if (propertyIdToActor.TryGetValue(addMsg.PropertyId, out var actorRef))
                    {
                        actorRef.Forward(addMsg);
                    }
                    else
                    {
                        Log.Info($"Creating property {addMsg.PropertyId}");
                        var propertyActor = Context.ActorOf(Property.Props(addMsg.RegionId, addMsg.PropertyId), $"property-{addMsg.PropertyId}");
                        Context.Watch(propertyActor);
                        actorToPropertyId.Add(propertyActor, addMsg.PropertyId);
                        propertyIdToActor.Add(addMsg.PropertyId, propertyActor);
                        propertyActor.Forward(addMsg);
                    }
                    break;
                case RequestAddProperty addMsg:
                    Log.Warning($"Ignoring Add Property request for {addMsg.RegionId}. This actor is responsible for {RegionId}.");
                    break;
                case GetPropertyListRequest listMsg:
                    if (listMsg.StartDate == null)
                    {
                        Sender.Tell(new PropertyListResponse(listMsg.RequestId, new HashSet<int>(propertyIdToActor.Keys)));
                    }
                    else
                    {
                        Log.Info($"TODO: Forward to properties and handle response or maybe is another action...");
                    }
                    break;
                case Terminated t:
                    var propertyId = actorToPropertyId[t.ActorRef];
                    Log.Info($"Property actor for {propertyId} has been terminated");
                    actorToPropertyId.Remove(t.ActorRef);
                    propertyIdToActor.Remove(propertyId);
                    break;
                //case RequestAllTemperatures r:
                //    Context.ActorOf(DeviceGroupQuery.Props(actorToPropertyId, r.RequestId, Sender, TimeSpan.FromSeconds(3)));
                //    break;
            }
        }

    }

}
