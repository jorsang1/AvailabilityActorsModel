namespace TestDispoActors.Actors.Messages
{
    public sealed class RequestDeleteProperty
    {
        public int RegionId { get; }
        public int PropertyId { get; }

        public RequestDeleteProperty(int regionId, int propertyId)
        {
            RegionId = regionId;
            PropertyId = propertyId;
        }

    }
}
