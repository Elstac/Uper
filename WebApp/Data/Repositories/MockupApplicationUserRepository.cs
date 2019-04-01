using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Repositories
{
    public class MockupApplicationUserRepository<T> : IApplicationUserRepository<T>
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

        public T GetById(string id)
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
