using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Data.Specifications
{
    public class TravelListSpecification : BaseSpecification<TripDetails>
    {
        public TravelListSpecification(Expression<Func<TripDetails, bool>> criteria) : base(criteria)
        {
        }
    }
}
