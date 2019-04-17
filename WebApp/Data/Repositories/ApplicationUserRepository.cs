using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApp.Data.Repositories
{
    public interface IApplicationUserRepository :IRepository<ApplicationUser,string>
    {
        ApplicationUser GetUserWithTripListById(string id);
    }

    public class ApplicationUserRepository : BaseRepository<ApplicationUser, string>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationContext context):base(context)
        {

        }

        public ApplicationUser GetUserWithTripListById(string id)
        {
            return context.Users.Include(au => au.TripList).Where(au => au.Id == id).FirstOrDefault();
        }
    }
}
