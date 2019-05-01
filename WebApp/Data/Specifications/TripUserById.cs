using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class TripUserById : BaseSpecification<TripUser>
    {
        public TripUserById(int id) : base(
            user => user.TripId == id)
        {

        }
    }
}
