using System;

namespace WebApp.Data.Specifications
{
    public class TripDetailsByDriverIdAndDateEndHigherThanDateNow:BaseSpecification<TripDetails>
    {
        public TripDetailsByDriverIdAndDateEndHigherThanDateNow(string driverId) : base
           (td => td.DriverId.Equals(driverId) && td.DateEnd.CompareTo(DateTime.Now) > 0)
        { }
    }
}
