using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.EmailConfirmation
{
    public interface IConfirmationProvider
    {
        Task ConfirmAsync(ApplicationUser user, string token, params object[] parameters);
    }

    public class AccountConfirmationProvider : IConfirmationProvider
    {
        private UserManager<ApplicationUser> userManager;

        public AccountConfirmationProvider(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task ConfirmAsync(ApplicationUser user, string token, params object[] parameters)
        {
            await userManager.ConfirmEmailAsync(user, token);
        }
    }
}
