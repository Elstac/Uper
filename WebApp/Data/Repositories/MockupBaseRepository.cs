using System.Collections.Generic;
using System.Linq;

namespace WebApp.Data.Repositories
{
    /// <summary>
    /// Base class for every mockup repository. Every mockupRepository implementation should inherit from this class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MockupBaseRepository<T> : IRepository<T>
    {
        protected List<T> dbContext;

        public void Add(T toAdd)
        {
            dbContext.Add(toAdd);
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext;
        }

        public T GetById(int id)
        {
            return dbContext[id];
        }

        public IEnumerable<T> GetList(ISpecification<T> specification)
        {
            return dbContext.AsQueryable().Where(specification.Criteria);
        }

        public void Remove(T toRemove)
        {
            dbContext.Remove(toRemove);
        }
    }
}
