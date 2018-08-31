using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using TestDispoActors.Actors.Messages;

namespace TestDispoActors.Actors
{
    public class AvailabilityManager : UntypedActor
    {
        private Dictionary<int, IActorRef> regionIdToActor = new Dictionary<int, IActorRef>();
        private Dictionary<IActorRef, int> actorToRegionId = new Dictionary<IActorRef, int>();

        public static Props Props(int regionId) => Akka.Actor.Props.Create<AvailabilityManager>();

        protected override void PreStart() => Log.Info("AvailabilityManager started");
        protected override void PostStop() => Log.Info("AvailabilityManager stopped");

        protected ILoggingAdapter Log { get; } = Context.GetLogger();

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case RequestAddProperty addMsg:
                    if (regionIdToActor.TryGetValue(addMsg.RegionId, out var actorRef))
                    {
                        actorRef.Forward(addMsg);
                    }
                    else
                    {
                        Log.Info($"Creating Region actor for {addMsg.RegionId}");
                        var regionActor = Context.ActorOf(Region.Props(addMsg.RegionId), $"region-{addMsg.RegionId}");
                        Context.Watch(regionActor);
                        regionActor.Forward(addMsg);
                        regionIdToActor.Add(addMsg.RegionId, regionActor);
                        actorToRegionId.Add(regionActor, addMsg.RegionId);
                    }
                    break;
                case RequestDeleteProperty msg:
                    if (regionIdToActor.TryGetValue(msg.RegionId, out var regionActorRefForDeleting))
                    {
                        regionActorRefForDeleting.Forward(msg);
                    }
                    else
                    {
                        Log.Info($"There is no such a Region {msg.RegionId}");
                    }
                    break;
                case GetPropertyListRequest listMsg:
                    if (regionIdToActor.TryGetValue(listMsg.RegionId, out var regionActorRef))
                    {
                        regionActorRef.Forward(listMsg);
                    }
                    else
                    {
                        Log.Info($"There is no such a Region {listMsg.RegionId}");
                    }
                    break;
                case Terminated t:
                    var regionId = actorToRegionId[t.ActorRef];
                    Log.Info($"Region actor for {regionId} has been terminated");
                    actorToRegionId.Remove(t.ActorRef);
                    regionIdToActor.Remove(regionId);
                    break;
            }
        }

    }
}
