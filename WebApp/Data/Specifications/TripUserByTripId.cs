using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class TripUserByTripId : BaseSpecification<TripUser>
    {
        public TripUserByTripId(int tripId) : base(
            user => user.TripId == tripId)
        {

        }
    }
}
