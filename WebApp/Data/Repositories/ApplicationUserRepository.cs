using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.Data.Specifications.Infrastructure;

namespace WebApp.Data.Repositories
{
    public interface IApplicationUserRepository :IRepository<ApplicationUser,string>
    {
        ApplicationUser GetUserWithTripListById(string id);
    }

    public class ApplicationUserRepository : BaseRepository<ApplicationUser, string>, IApplicationUserRepository
    {
        public ApplicationUserRepository(
            ApplicationContext context
            ,ISpecificationEvaluator<ApplicationUser> specificationEvaluator)
            :base(context,specificationEvaluator)
        {

        }

        public ApplicationUser GetUserWithTripListById(string id)
        {
            return context.Users.Include(au => au.TripList)
                .ThenInclude(td => td.Trip)
                .Where(au => au.Id == id)
                .FirstOrDefault();
        }
    }
}
