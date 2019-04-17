using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Data.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }

        /// <summary>
        /// Apply order by expression to query
        /// </summary>
        /// <param name="orderByExpression"></param>
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        /// <summary>
        /// Apply order by descending expression to query
        /// </summary>
        /// <param name="orderByDescendingExpression"></param>
        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        /// <summary>
        /// Apply pageing to query. Query returns <paramref name="take"/> objects starting at <paramref name="skip"/> position.
        /// </summary>
        /// <param name="take">Nuber of returned objects</param>
        /// <param name="skip">Positin of starting object</param>
        protected virtual void ApplyPageing(int take,int skip)
        {
            Take = take;
            Skip = skip; 
        }
    }
}
