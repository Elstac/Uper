using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class IncludeTestSpecification : BaseSpecification<TripDetails>
    {
        public IncludeTestSpecification() : base(null)
        {
            AddInclude<TripUser>(td => td.Passangers)
                .AddThenInclude(tu => tu.User);
        }
    }
}
