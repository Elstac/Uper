using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Exceptions;

namespace WebApp.Models.EmailConfirmation
{
    public class PasswordResetProvider : IConfirmationProvider
    {
        private UserManager<ApplicationUser> userManager;

        public PasswordResetProvider(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task ConfirmAsync(ApplicationUser user, string token, params object[] parameters)
        {
            if (parameters == null)
                throw new OwnNullArgumentException("Invalid parameters number");

            var result = await userManager.ResetPasswordAsync(user, token, parameters[0].ToString());

            if (!result.Succeeded)
                throw new InvalidOperationException("Password reset failed");
        }
    }
}
