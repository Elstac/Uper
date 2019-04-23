using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Repositories
{
    /// <summary>
    /// Provides access to TripDetails table in database
    /// </summary>
    public interface ITripUserRepository : IRepository<TripUser, int>
    {

    }

    public class TripUserRepository : BaseRepository<TripUser, int>, ITripUserRepository
    {
        public TripUserRepository(ApplicationContext context) : base(context)
        {

        }

    }
}
