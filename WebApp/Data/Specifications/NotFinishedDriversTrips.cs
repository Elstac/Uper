using System;

namespace WebApp.Data.Specifications
{
    public class NotFinishedDriversTrips:BaseSpecification<TripDetails>
    {
        public NotFinishedDriversTrips(string driverId) : base
           (td => td.DriverId.Equals(driverId) && td.DateEnd.CompareTo(DateTime.Now) > 0)
        {
            ApplyOrderByDescending(td => td.Date);
        }
    }
}
