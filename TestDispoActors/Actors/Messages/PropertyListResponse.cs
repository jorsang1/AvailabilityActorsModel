using System.Collections.Generic;

namespace TestDispoActors.Actors.Messages
{
    public sealed class PropertyListResponse
    {
        public long RequestId { get; }
        public ISet<int> Ids { get; }

        public PropertyListResponse(long requestId, ISet<int> ids)
        {
            RequestId = requestId;
            Ids = ids;
        }

    }
}
