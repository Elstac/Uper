using System;
using System.Linq.Expressions;

namespace WebApp.Data.Specifications
{
    public class ApiTripDetailsSpecification : ISpecification<TripDetails>
    {
        public Expression<Func<TripDetails, bool>> Criteria { get; };

        public Expression<Func<TripDetails, object>> OrderBy => throw new NotImplementedException();

        public Expression<Func<TripDetails, object>> OrderByDescending => throw new NotImplementedException();

        public int Take { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
