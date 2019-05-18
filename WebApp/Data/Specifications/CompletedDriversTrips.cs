using System;

namespace WebApp.Data.Specifications
{
    public class FinishedDriversTrips : BaseSpecification<TripDetails>
    {
        public FinishedDriversTrips(string driverId):base
            (td=>td.DriverId.Equals(driverId) && td.DateEnd.CompareTo(DateTime.Now)<=0)
        {
            ApplyOrderByDescending(td => td.DateEnd);
        }
    }
}
