using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class TripUserByUserId : BaseSpecification<TripUser>
    {
        public TripUserByUserId(string uid) : base(
            tu => tu.UserId.Equals(uid))
        {

        }
    }
}

