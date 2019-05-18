using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications.Infrastructure
{
    public class TripUserCollectionIncluder : BaseIncluder
    {
        public TripUserCollectionIncluder() : base()
        {
        }

        public override IQueryable<T> ApplyInclude<T>(IQueryable<T> query, LambdaExpression includeExpression, LambdaExpression thenIncludeExpression, Type memberType)
        {
            if(memberType != typeof(ICollection<TripUser>))
                return Next(query, includeExpression, thenIncludeExpression, memberType);

            if(thenIncludeExpression != null)
            {
                var convInclude = includeExpression as Expression<Func<T, IEnumerable<TripUser>>>;
                var convThenInclude = thenIncludeExpression as Expression<Func<TripUser, object>>;

                return query.Include(convInclude).ThenInclude(convThenInclude);
            }
            else
            {
                var convInclude = includeExpression as Expression<Func<T, ICollection<TripUser>>>;
                return query.Include(convInclude);
            }
        }
    }
}
