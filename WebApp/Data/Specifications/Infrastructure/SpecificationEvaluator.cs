using System.Collections.Generic;
using System.Linq;

namespace WebApp.Data.Specifications.Infrastructure
{
    public interface ISpecificationEvaluator
    {
        IEnumerable<T> EvaluateSpecification<T>(IQueryable<T> query, ISpecification<T> specification) where T : class;
    }

    /// <summary>
    /// Generic class for applaying specification to given queries
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SpecificationEvaluator : ISpecificationEvaluator
    {
        private IIncludeManager includeManager;

        public SpecificationEvaluator(IIncludeManager includeManager)
        {
            this.includeManager = includeManager;
        }

        /// <summary>
        /// Apply given specification to given query
        /// </summary>
        /// <param name="query">Base query</param>
        /// <param name="specification">Specification to apply</param>
        /// <returns>New query containing specification</returns>
        public IEnumerable<T> EvaluateSpecification<T>(IQueryable<T> query, ISpecification<T> specification) where T:class
        {
            var ret = query;
            if(specification.Criteria !=null)
            {
                ret = ret.Where(specification.Criteria);
            }

            ret = includeManager.ApplyIncludeChains(ret,specification.IncludeChains);

            if (specification.Take != 0)
                ret = ret.Take(specification.Take);

            if (specification.OrderBy != null)
                ret = ret.OrderBy(specification.OrderBy);

            if (specification.OrderByDescending != null)
                ret = ret.OrderBy(specification.OrderByDescending);

            return ret.ToList();
        }
        
    }
}
