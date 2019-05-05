using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class TripUserByTripId : BaseSpecification<TripUser>
    {
        public TripUserByTripId(int id) : base(
            user => user.TripId == id)
        {

        }
    }
}
