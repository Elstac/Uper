using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Repositories
{
    public class MockupApplicationUserRepository : IApplicationUserRepository
    {
        protected List<ApplicationUser> dbContext;

        public void Add(ApplicationUser toAdd)
        {
            dbContext.Add(toAdd);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return dbContext;

        }

        public ApplicationUser GetById(string id)
        {
            if ( Int32.TryParse(id , out int intId) )
            {
                return dbContext[intId];
            }
            else
            {
                throw new Exception("Wrong Id");
            }
        }

        public IEnumerable<ApplicationUser> GetList(ISpecification<ApplicationUser> specification)
        {
            return dbContext.AsQueryable().Where(specification.Criteria);
        }

        public void Remove(ApplicationUser toRemove)
        {
            dbContext.Remove(toRemove);
        }
    }
}
