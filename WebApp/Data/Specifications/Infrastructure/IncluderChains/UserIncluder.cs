using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebApp.Data.Specifications.Infrastructure
{
    public class UserIncluder : BaseIncluder
    {
        public UserIncluder() : base()
        {
        }

        public override IQueryable<T> ApplyInclude<T>(IQueryable<T> query, LambdaExpression includeExpression, LambdaExpression thenIncludeExpression, Type memberType)
        {
            if (thenIncludeExpression != null || memberType != typeof(ApplicationUser))
                return Next(query, includeExpression, thenIncludeExpression, memberType);

            var convInclude = includeExpression as Expression<Func<T, ApplicationUser>>;

            return query.Include(convInclude);
        }
    }
}
