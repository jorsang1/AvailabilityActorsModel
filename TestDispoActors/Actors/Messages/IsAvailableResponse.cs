namespace TestDispoActors.Actors.Messages
{
    public sealed class IsAvailableResponse
    {
        public long RequestId { get; }
        public bool IsAvailable { get; }

        public IsAvailableResponse(long requestId, bool isAvailable)
        {
            RequestId = requestId;
            IsAvailable = isAvailable;
        }
    }
}
