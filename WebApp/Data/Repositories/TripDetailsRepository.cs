using System.Collections.Generic;
using System.Linq;

namespace WebApp.Data.Repositories
{

    /// <summary>
    /// Provides access to TripDetails table in database
    /// </summary>
    public interface ITripDetailsRepository : IRepository<TripDetails>
    {

    }

    public class TripDetailsRepository:ITripDetailsRepository
    {
        private ApplicationContext dbContext;

        public TripDetailsRepository(ApplicationContext Context)
        {
            dbContext = Context;
        }

        public void Add(TripDetails toAdd)
        {
            dbContext.Set<TripDetails>().Add(toAdd);
             dbContext.SaveChanges();
        }

        public IEnumerable<TripDetails> GetAll()
        {
            return dbContext.Set<TripDetails>().ToList();
        }

        public TripDetails GetById(int id)
        {
            return dbContext.Set<TripDetails>().Find(id);
        }

        public IEnumerable<TripDetails> GetList(ISpecification<TripDetails> specification)
        {
            return dbContext.Set<TripDetails>().AsQueryable().Where(specification.Criteria).ToList();
        }

        public void Remove(TripDetails toRemove)
        {
            dbContext.Set<TripDetails>().Remove(toRemove);
            dbContext.SaveChanges();
        }
    }
}
