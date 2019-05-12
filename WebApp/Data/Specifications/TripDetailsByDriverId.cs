namespace WebApp.Data.Specifications
{
    public class TripDetailsByDriverId : BaseSpecification<TripDetails>
    {
        public TripDetailsByDriverId(string driverId) : base(
            td => td.DriverId.Equals(driverId))
        {
        }
    }
}


