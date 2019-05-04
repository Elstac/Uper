using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Specifications;

namespace WebApp.Data.Repositories
{

    /// <summary>
    /// Provides access to TripDetails table in database
    /// </summary>
    public interface ITripDetailsRepository : IRepository<TripDetails,int>
    {
        TripDetails GetTripWithPassengersById(int id);
        IEnumerable<TripDetails> GetListWithPassengers(ISpecification<TripDetails> specification);
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

        public IEnumerable<TripDetails> GetListWithPassengers(ISpecification<TripDetails> specification)
        {
            return SpecificationEvaluator<TripDetails>.EvaluateSpecification(
                context.Set<TripDetails>()
                    .Include(td => td.Passangers)
                    .ThenInclude(td => td.User)
                , specification);
        }
    }
}
