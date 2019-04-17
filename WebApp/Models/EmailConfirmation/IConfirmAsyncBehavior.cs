using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.EmailConfirmation
{
    public interface IConfirmAsyncBehavior
    {
        Task ConfirmAsync(ApplicationUser user,string token, params object[] parameters);
    }
}