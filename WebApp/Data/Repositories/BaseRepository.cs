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

        /// <summary>
        /// Main method for repository. Provides base db query for concrete repository. 
        /// For simple data object it sholud return DbSet.AsQuerable().
        /// </summary>
        /// <returns></returns>
        protected abstract IQueryable<T> GetBaseQuery();

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
            return GetBaseQuery().ToList();
        }

        public T GetById(int id)
        {
            return GetBaseQuery().Where(be => be.Id == id).First();
        }

        public IEnumerable<T> GetList(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.EvaluateSpecification(
                GetBaseQuery(),
                specification);
        }

        public void Remove(T toRemove)
        {
            context.Set<T>().Remove(toRemove);
            context.SaveChanges();
        }
    }
}
