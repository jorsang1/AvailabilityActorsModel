using System;

namespace TestDispoActors.Actors.Messages
{
    public sealed class IsAvailableRequest
    {
        public long RequestId { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public IsAvailableRequest(long requestId, DateTime startDate, DateTime endDate)
        {
            RequestId = requestId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
