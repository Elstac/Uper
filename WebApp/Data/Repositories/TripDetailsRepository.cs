namespace WebApp.Data.Repositories
{

    /// <summary>
    /// Provides access to TripDetails table in database
    /// </summary>
    public interface ITripDetailsRepository : IRepository<TripDetails,int>
    {

    }

    public class TripDetailsRepository:BaseRepository<TripDetails,int>, ITripDetailsRepository
    {
        public TripDetailsRepository(ApplicationContext context):base(context)
        {

        }
    }
}
