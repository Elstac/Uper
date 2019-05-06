using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Data.Specifications.Infrastructure;

namespace WebApp.Data.Repositories
{
    /// <summary>
    /// Provides access to TripDetails table in database
    /// </summary>
    public interface ITripUserRepository : IRepository<TripUser, (int, string)>
    {
        void RemoveUserFromTrip(int id,string userid);
        void RemoveTripUsers(int id);
    }

    public class TripUserRepository : BaseRepository<TripUser, (int,string)>, ITripUserRepository
    {
        public TripUserRepository(ApplicationContext context
            ,ISpecificationEvaluator specificationEvaluator)
            : base(context,specificationEvaluator)
        {

        }

        public void RemoveUserFromTrip(int id, string userid)
        {
                context.Set<TripUser>().Remove(context.Set<TripUser>().Find(id,userid));
                context.SaveChanges();
        }

        public void RemoveTripUsers(int id)
        {
            context.Set<TripUser>().RemoveRange(context.TripUser.Where(tu => tu.TripId == id));
            context.SaveChanges();
        }

        public override TripUser GetById((int, string) id)
        {
            return context.Set<TripUser>().Find(id.Item1, id.Item2);
        }
    }
}
