using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.EmailConfirmation
{
    public interface IConfirmationProvider
    {
        Task ConfirmAsync(ApplicationUser user,string token, params object[] parameters);
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}