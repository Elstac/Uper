using System;
using System.Linq.Expressions;

namespace WebApp.Data
{
    /// <summary>
    /// Provides inteface for classes containing where expressions for bd queries
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public interface ITravelListSpecification<T> 
    {
        /// <summary>
        /// Where clause in query
        /// </summary>
        Expression<Func<T,bool>> Criteria { get; }
        /// <summary>
        /// Order by clause in query
        /// </summary>
        Expression<Func<T,object>> OrderBy { get; }
        /// <summary>
        /// Order by clause in query in descending variation
        /// </summary>
        Expression<Func<T,object>> OrderByDescending { get; }
        /// <summary>
        /// Number of rows included in returned collection
        /// </summary>
        int Take { get; }
        /// <summary>
        /// Nuber of first row included in returned collection
        /// </summary>
        int Skip { get; }
    }
}
