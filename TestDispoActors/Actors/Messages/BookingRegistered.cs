namespace TestDispoActors.Actors.Messages
{
    public sealed class BookingRegistered
    {
        public long RequestId { get; }
        public BookingRegistered(long requestId)
        {
            RequestId = requestId;
        }
    }
}
