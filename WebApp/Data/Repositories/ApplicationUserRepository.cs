namespace WebApp.Data.Repositories
{
    public interface IApplicationUserRepository :IRepository<ApplicationUser,string>
    {
       
    }

    public class ApplicationUserRepository : BaseRepository<ApplicationUser, string>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationContext context):base(context)
        {

        }
    }
}
