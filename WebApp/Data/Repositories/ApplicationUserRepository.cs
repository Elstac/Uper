using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            return dbContext.Set<ApplicationUser>().Include(au=>au.TripList).ToList();
        }

        public ApplicationUser GetById(string id)
        {
            return dbContext.Set<ApplicationUser>().Include(au => au.TripList)
                .Where(au => au.Id == id).First();
        }

        public IEnumerable<ApplicationUser> GetList(ISpecification<ApplicationUser> specification)
        {
            return Specifications.SpecificationEvaluator<ApplicationUser>.EvaluateSpecification(
                dbContext.Set<ApplicationUser>().Include(au => au.TripList),
                specification);
        }

        public void Remove(ApplicationUser toRemove)
        {
            dbContext.Set<ApplicationUser>().Remove(toRemove);
            dbContext.SaveChanges();
        }
    }
}
