using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Data.Specifications.Infrastructure
{
    public abstract class BaseIncluder : IIncluder
    {
        private IIncluder next;

        public BaseIncluder(IIncluder next)
        {
            this.next = next;
        }

        public abstract IQueryable<T> ApplyInclude<T>(
            IQueryable<T> query, 
            LambdaExpression includeExpression, 
            LambdaExpression thenIncludeExpression, 
            Type memberType) where T:class;

        protected IQueryable<T> Next<T>(IQueryable<T> query,
            LambdaExpression includeExpression,
            LambdaExpression thenIncludeExpression,
            Type memberType) where T:class
        {
            if (next == null)
                throw new NotImplementedException(
                    $"No valid convertion implemeted. Include: {includeExpression}, ThenInlude: {thenIncludeExpression}, member type: {memberType}");

            return next.ApplyInclude(query, includeExpression, thenIncludeExpression,memberType);
        }
    }
}
