using System;
using System.Linq.Expressions;

namespace WebApp.Data
{
    /// <summary>
    /// Provides inteface for classes containing where expressions for bd queries
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public interface ISpecification<T> where T:BaseEntity
    {
        Expression<Func<T,bool>> Criteria { get; }
    }
}
