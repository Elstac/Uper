using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Repositories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetAll();
        void Add(ApplicationUser toAdd);
        void Remove(ApplicationUser toRemove);
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetList(ISpecification<ApplicationUser> specification);
    }

    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private ApplicationContext dbContext;

        public ApplicationUserRepository(ApplicationContext Context)
        {
            dbContext = Context;
        }
        public void Add(ApplicationUser toAdd)
        {
            dbContext.Set<ApplicationUser>().Add(toAdd);
            dbContext.SaveChanges();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return dbContext.Set<ApplicationUser>().ToList();
        }

        public ApplicationUser GetById(string id)
        {
            return dbContext.Set<ApplicationUser>().Find(id);
        }

        public IEnumerable<ApplicationUser> GetList(ISpecification<ApplicationUser> specification)
        {
            return Specifications.SpecificationEvaluator<ApplicationUser>.EvaluateSpecification(
                dbContext.Set<ApplicationUser>().AsQueryable(),
                specification);
        }

        public void Remove(ApplicationUser toRemove)
        {
            dbContext.Set<ApplicationUser>().Remove(toRemove);
            dbContext.SaveChanges();
        }
    }
}
