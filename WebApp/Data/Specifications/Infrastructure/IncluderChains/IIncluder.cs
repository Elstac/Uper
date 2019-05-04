using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebApp.Data.Specifications.Infrastructure
{
    public interface IIncluder
    {
        IQueryable<T> ApplyInclude<T>(
            IQueryable<T> query,
            LambdaExpression includeExpression,
            LambdaExpression thenIncludeExpression,
            Type memberType) where T:class;
    }
}
