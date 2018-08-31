using Akka.Actor;
using Akka.Event;
using TestDispoActors.Actors.Messages;

namespace TestDispoActors.Actors
{
    public partial class Property : UntypedActor
    {
        protected ILoggingAdapter Log { get; } = Context.GetLogger();
        protected int PropertyId { get; }

        public Property(int propertyId)
        {
            PropertyId = propertyId;
        }

        public static Props Props(int propertyId) => Akka.Actor.Props.Create(() => new Property(propertyId));

        protected override void PreStart() => Log.Info($"Property actor {PropertyId} started");
        protected override void PostStop() => Log.Info($"Property actor {PropertyId} stopped");

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case RequestAddProperty req when req.PropertyId.Equals(PropertyId):
                    Sender.Tell(PropertyRegistered.Instance);
                    break;
                case RequestAddProperty req:
                    Log.Warning($"Ignoring AddProperty request for {req.PropertyId}.This actor is responsible for {PropertyId}.");
                    break;
                case AddBooking req when req.PropertyId.Equals(PropertyId):
                    Log.Info($"Recorded unavailable dates {req.StartDate} - {req.EndDate} with {req.RequestId}");
                    RegisterNewBooking(req);
                    Sender.Tell(new BookingRegistered(req.RequestId));
                    break;
                case AddBooking req:
                    Log.Warning($"Ignoring AddBooking request for {req.PropertyId}.This actor is responsible for {PropertyId}.");
                    break;
                case IsAvailableRequest req:
                    Sender.Tell(new IsAvailableResponse(req.RequestId, IsAvailable(req.StartDate, req.EndDate)));
                    break;
            }
        }

    }
}
