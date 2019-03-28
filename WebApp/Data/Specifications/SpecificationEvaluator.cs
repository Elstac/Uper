using System.Linq;

namespace WebApp.Data.Specifications
{
    /// <summary>
    /// Generic class for applaying specification to given queries
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SpecificationEvaluator<T> where T:BaseEntity
    {
        /// <summary>
        /// Apply given specification to given query
        /// </summary>
        /// <param name="query">Base query</param>
        /// <param name="specification">Specification to apply</param>
        /// <returns>New query containing specification</returns>
        public static IQueryable<T> EvaluateSpecification(IQueryable<T> query, ISpecification<T> specification)
        {
            var ret = query;

            if(specification.Criteria !=null)
            {
                ret = ret.Where(specification.Criteria);
            }

            return ret;
        }
    }
}
