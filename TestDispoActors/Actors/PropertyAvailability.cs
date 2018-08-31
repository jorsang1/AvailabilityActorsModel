using System;
using System.Collections.Generic;
using Akka.Actor;
using TestDispoActors.Actors.Messages;
using TestDispoActors.DomainEntities;

namespace TestDispoActors.Actors
{
    public partial class Property : UntypedActor
    {
        private readonly SortedSet<DateTime> _unavailableDates = new SortedSet<DateTime>();
        private readonly Dictionary<DateTime, Booking> _bookingsByStartDate = new Dictionary<DateTime, Booking>();

        protected void RegisterNewBooking(AddBooking request)
        {
            _bookingsByStartDate.Add(request.StartDate, new Booking(request.BookingId, request.Reference, request.StartDate, request.EndDate, request.PropertyId));
            for (var currentDate = request.StartDate; currentDate < request.EndDate; currentDate.AddDays(1))
            {
                if (!_unavailableDates.Contains(currentDate)) _unavailableDates.Add(currentDate);
            }
        }

        protected bool IsAvailable(DateTime startDate, DateTime endDate)
        {
            //TODO: Make it well.
            return !(_unavailableDates.Contains(startDate) || _unavailableDates.Contains(endDate));
        }

    }
}
