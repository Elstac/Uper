using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Specifications;

namespace WebApp.Data.Repositories
{
    /// <summary>
    /// Base class for every repository. Implements shared functionality such as adding, removing, finding by id etc.
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private ApplicationContext context;

        public BaseRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(T toAdd)
        {
            context.Set<T>().Add(toAdd);
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetList(ISpecification<T> specification)
        {
            return ApplySpecification(specification,context.Set<T>()).ToList();
        }

        public void Remove(T toRemove)
        {
            context.Set<T>().Remove(toRemove);
            context.SaveChanges();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification,IQueryable<T> query)
        {
            return SpecificationEvaluator<T>.EvaluateSpecification(query, specification);
        }
    }
}
