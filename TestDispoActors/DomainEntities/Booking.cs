using System;

namespace TestDispoActors.DomainEntities
{
    public class Booking
    {
        public long Id { get; }
        public string Reference { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int PropertyId { get; }

        public Booking(long id, string reference, DateTime startDate, DateTime endDate, int propertyId)
        {
            Id = id;
            Reference = reference;
            StartDate = startDate;
            EndDate = endDate;
            PropertyId = propertyId;
        }
    }
}
