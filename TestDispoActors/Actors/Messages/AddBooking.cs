using System;

namespace TestDispoActors.Actors.Messages
{
    public sealed class AddBooking
    {
        public long RequestId { get; }
        public int PropertyId { get; }
        public int BookingId { get; }
        public string Reference { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public AddBooking(long requestId, int propertyId, DateTime startDate, DateTime endDate, string reference)
        {
            RequestId = requestId;
            BookingId = BookingId;
            PropertyId = propertyId;
            StartDate = startDate;
            EndDate = endDate;
            Reference = reference;
        }

    }
}
