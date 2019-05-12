using System;

namespace WebApp.Data.Specifications
{
    public class TripDetailsByDriverIdAndDateEndLowerThanDateNow : BaseSpecification<TripDetails>
    {
        public TripDetailsByDriverIdAndDateEndLowerThanDateNow(string driverId):base
            (td=>td.DriverId.Equals(driverId) && td.DateEnd.CompareTo(DateTime.Now)<=0)
        {}
    }
}
