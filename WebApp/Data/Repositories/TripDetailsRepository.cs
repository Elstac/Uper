using Microsoft.EntityFrameworkCore;
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

    public class TripDetailsRepository:BaseRepository<TripDetails>, ITripDetailsRepository
    {
        public TripDetailsRepository(ApplicationContext context):base(context)
        {
        }

        protected override IQueryable<TripDetails> GetBaseQuery()
        {
            return context.TripDetails.Include(td => td.Passangers);
        }
    }
}
