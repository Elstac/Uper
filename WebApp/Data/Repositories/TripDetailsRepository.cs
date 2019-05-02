using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WebApp.Data.Repositories
{

    /// <summary>
    /// Provides access to TripDetails table in database
    /// </summary>
    public interface ITripDetailsRepository : IRepository<TripDetails,int>
    {
        TripDetails GetTripWithPassengersById(int id);
    }

    public class TripDetailsRepository:BaseRepository<TripDetails,int>, ITripDetailsRepository
    {
        public TripDetailsRepository(ApplicationContext context):base(context)
        {

        }

        public TripDetails GetTripWithPassengersById(int id)
        {
             return context.TripDetails.Include(td => td.Passangers)
                .ThenInclude(td  => td.User)
                .Where(td => td.Id == id)
                .FirstOrDefault();
        }
    }
}
