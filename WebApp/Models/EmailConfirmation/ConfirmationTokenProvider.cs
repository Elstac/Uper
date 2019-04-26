using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.EmailConfirmation
{
    public interface IConfirmationTokenProvider
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }

    public class AccountTokenProvider:IConfirmationTokenProvider
    {
        private UserManager<ApplicationUser> userManager;

        public AccountTokenProvider(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }
    }
}
