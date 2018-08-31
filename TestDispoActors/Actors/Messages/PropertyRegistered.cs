namespace TestDispoActors.Actors.Messages
{
    public sealed class PropertyRegistered
    {
        public static PropertyRegistered Instance { get; } = new PropertyRegistered();
        private PropertyRegistered() { }
    }
}