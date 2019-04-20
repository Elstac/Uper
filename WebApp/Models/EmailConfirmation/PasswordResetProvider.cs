using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.EmailConfirmation
{
    public class PasswordResetProvider : IConfirmationProvider
    {
        private UserManager<ApplicationUser> userManager;

        public Task ConfirmAsync(ApplicationUser user, string token, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
