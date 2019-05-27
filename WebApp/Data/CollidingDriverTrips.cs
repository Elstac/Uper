using System;
using WebApp.Data.Entities;
using WebApp.Data.Specifications;

namespace WebApp.Data
{
    public class CollidingDriverTrips : BaseSpecification<TripDetails>
    {
        public CollidingDriverTrips(string driverId, DateTime dateStart, DateTime dateEnd) : base
    (
            td => td.DriverId.Equals(driverId)&&
            (
            (td.Date.CompareTo(dateStart) >= 0 && td.Date.CompareTo(dateEnd) <= 0) ||
             (td.DateEnd.CompareTo(dateStart) >= 0 && td.DateEnd.CompareTo(dateEnd) <= 0) ||
             (td.Date.CompareTo(dateStart) <= 0 && td.DateEnd.CompareTo(dateEnd) >= 0)
            )
            )
        {
            AddInclude<TripUser>(td => td.Passangers)
                   .AddThenInclude(tu => tu.User);
            ApplyOrderByDescending(td => td.DateEnd);
        }

    }
}
