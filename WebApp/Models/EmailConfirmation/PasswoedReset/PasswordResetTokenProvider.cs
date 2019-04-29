using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.EmailConfirmation
{
    public class PasswordResetTokenProvider : IConfirmationTokenProvider
    {
        private UserManager<ApplicationUser> userManager;

        public PasswordResetTokenProvider(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}
