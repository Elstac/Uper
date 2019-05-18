using System;
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class NotFinishedDriversTrips:BaseSpecification<TripDetails>
    {
        public NotFinishedDriversTrips(string driverId) : base
           (td => td.DriverId.Equals(driverId) && td.DateEnd.CompareTo(DateTime.Now) > 0)
        {
            AddInclude<TripUser>(td => td.Passangers)
                .AddThenInclude(tu => tu.User);

            ApplyOrderByDescending(td => td.Date);
        }
    }
}
