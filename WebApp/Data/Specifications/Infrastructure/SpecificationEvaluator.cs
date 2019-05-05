using System.Collections.Generic;
using System.Linq;

namespace WebApp.Data.Specifications.Infrastructure
{
    public interface ISpecificationEvaluator<T> where T : class
    {
        IEnumerable<T> EvaluateSpecification(IQueryable<T> query, ISpecification<T> specification);
    }

    /// <summary>
    /// Generic class for applaying specification to given queries
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SpecificationEvaluator<T> : ISpecificationEvaluator<T> where T:class
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
        public IEnumerable<T> EvaluateSpecification(IQueryable<T> query, ISpecification<T> specification)
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
