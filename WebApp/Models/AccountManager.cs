using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models
{
    public interface IAccountManager
    {
        Task CreateAccountAsync(string username, string password, string email);
        Task SignInAsync(string username, string password);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal);
        bool IsSignedIn(ClaimsPrincipal principal);
        Task SignOutAsync();
        string GetUserName(ClaimsPrincipal principal);

    }

    public class AccountManager : IAccountManager
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IIdentityResultErrorHtmlCreator errorCreator;
        private IEmailAddressValidator validator;

        public AccountManager(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IIdentityResultErrorHtmlCreator errorCreator,
                              IEmailAddressValidator validator)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.errorCreator = errorCreator;
            this.validator = validator;
        }

        public async Task CreateAccountAsync(string username, string password, string email)
        {
            if (!validator.ValidateEmailAddress(email))
                return;
            
            var result = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = username,
                Email = email
            }, password);

            if (!result.Succeeded)
                throw new InvalidOperationException(errorCreator.CreateErrorHtml(result));
        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await userManager.GetUserAsync(principal);
        }

        public async Task SignInAsync(string username, string password)
        {
            var result = await signInManager.PasswordSignInAsync(username, password, true, false);

            if (!result.Succeeded)
                throw new InvalidOperationException("Sign in failed");
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return signInManager.IsSignedIn(principal);
        }

        public string GetUserName(ClaimsPrincipal principal)
        {
            return userManager.GetUserName(principal);
        }
    }
}
