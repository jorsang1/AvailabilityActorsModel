using System;

namespace TestDispoActors.Actors.Messages
{
    public class GetPropertyListRequest
    {
        public long RequestId { get; }
        public int RegionId { get; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }

        public GetPropertyListRequest(long requestId, int regionId, DateTime? startDate, DateTime? endDate)
        {
            RequestId = requestId;
            RegionId = regionId;
            StartDate = startDate;
            EndDate = endDate;
        }

    }
}
