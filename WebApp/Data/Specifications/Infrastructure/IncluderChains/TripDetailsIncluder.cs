using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebApp.Data.Specifications.Infrastructure
{
    public class TripDetailsIncluder : BaseIncluder
    {
        public TripDetailsIncluder():base()
        {
        }

        public override IQueryable<T> ApplyInclude<T>(IQueryable<T> query, LambdaExpression includeExpression, LambdaExpression thenIncludeExpression, Type memberType)
        {
            if (thenIncludeExpression != null || memberType != typeof(TripDetails))
                return Next(query, includeExpression, thenIncludeExpression, memberType);

            var convInclude = includeExpression as Expression<Func<T, TripDetails>>;

            return query.Include(convInclude);
        }
    }
}
