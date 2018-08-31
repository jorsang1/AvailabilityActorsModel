namespace TestDispoActors.Actors.Messages
{
    public sealed class RequestAddProperty
    {
        public int RegionId { get; }
        public int PropertyId { get; }

        public RequestAddProperty(int regionId, int propertyId)
        {
            RegionId = regionId;
            PropertyId = propertyId;
        }

    }
}
