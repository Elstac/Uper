using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Specifications;
using WebApp.Data.Specifications.Infrastructure;

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
        public TripDetailsRepository(ApplicationContext context
            ,ISpecificationEvaluator specificationEvaluator)
            :base(context,specificationEvaluator)
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
