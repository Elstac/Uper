using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class TripUserByUsernameAndTripId : BaseSpecification<TripUser>
    {
        public TripUserByUsernameAndTripId(int tripId, string username) :
            base(
                td => td.TripId == tripId &&
                td.User.UserName == username
            )
        {
            AddInclude(td => td.User);
            AddInclude(td => td.Trip);
        }
    }

}
