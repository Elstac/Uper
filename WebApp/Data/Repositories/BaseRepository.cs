using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Specifications;
using Microsoft.EntityFrameworkCore;
namespace WebApp.Data.Repositories
{
    /// <summary>
    /// Base class for every repository. Implements shared functionality such as adding, removing, finding by id etc.
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected ApplicationContext context;
        
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
            return context.Set<T>().Where(be => be.Id == id).First();
        }

        public IEnumerable<T> GetList(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.EvaluateSpecification(
                context.Set<T>(),
                specification);
        }

        public void Remove(T toRemove)
        {
            context.Set<T>().Remove(toRemove);
            context.SaveChanges();
        }
    }
}
