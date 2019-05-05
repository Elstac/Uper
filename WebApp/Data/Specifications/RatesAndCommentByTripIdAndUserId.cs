using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class RatesAndCommentByTripIdAndUserId : BaseSpecification<RatesAndComment>
    {
        public RatesAndCommentByTripIdAndUserId(string uid,int tid) : base(
          rat => (rat.UserId.Equals(uid) && rat.TripId == tid))
        {

        }
    }
}
