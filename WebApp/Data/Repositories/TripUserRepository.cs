using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Repositories
{
    public class TripUserRepository : ITripUserRepository
    {
        private ApplicationContext dbContext;

        public TripUserRepository(ApplicationContext Context)
        {
            dbContext = Context;
        }
        public void Add(TripUser toAdd)
        {
            dbContext.Set<TripUser>().Add(toAdd);
            dbContext.SaveChanges();
        }

        public IEnumerable<TripUser> GetAll()
        {
            return dbContext.Set<TripUser>().ToList();
        }

        public TripUser GetById(int id)
        {
            return dbContext.Set<TripUser>().Find(id);
        }

        public IEnumerable<TripUser> GetList(ISpecification<TripUser> specification)
        {
            return dbContext.Set<TripUser>().AsQueryable().Where(specification.Criteria).ToList();
        }

        public void Remove(TripUser toRemove)
        {
            dbContext.Set<TripUser>().Remove(toRemove);
            dbContext.SaveChanges();
        }
    }
}
