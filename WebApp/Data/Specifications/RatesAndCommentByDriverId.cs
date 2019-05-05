
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class RatesAndCommentByDriverId : BaseSpecification<RatesAndComment>
    {
        public RatesAndCommentByDriverId(string did) : base(
          rat => rat.DriverId.Equals(did))
        {

        }
    }
}

