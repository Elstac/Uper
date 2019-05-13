using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models
{
    public interface IAccountManager
    {
        Task CreateAccountAsync(ApplicationUser user, string password);
        Task SignInAsync(string username, string password);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal);
        bool IsSignedIn(ClaimsPrincipal principal);
        Task SignOutAsync();
        string GetUserId(ClaimsPrincipal principal);
        string GetUserName(ClaimsPrincipal principal);
        bool CheckPassword(ApplicationUser user, string currentPassword);
        void ChangePassword(ApplicationUser user, string oldPassword, string newPassword);
        IdentityResult ValidatePassword(ApplicationUser user, string password);
    }

    public class AccountManager : IAccountManager
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IIdentityResultErrorCreator errorCreator;
        private IEmailAddressValidator validator;

        public AccountManager(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IIdentityResultErrorCreator errorCreator,
                              IEmailAddressValidator validator)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.errorCreator = errorCreator;
            this.validator = validator;
        }

        public async Task CreateAccountAsync(ApplicationUser user,string password)
        {
            if (!validator.ValidateEmailAddress(user.Email))
                return;
            
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new InvalidOperationException(errorCreator.CreateError(result));
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

        public string GetUserId(ClaimsPrincipal principal)
        {
            return userManager.GetUserId(principal);
        }

        public void ChangePassword(ApplicationUser user,string oldPassword,string newPassword)
        {
            userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public bool CheckPassword(ApplicationUser user,string currentPassword)
        {
            var result = userManager.CheckPasswordAsync(user, currentPassword);

            return result.Result;
        }

        public IdentityResult ValidatePassword(ApplicationUser user, string password)
        {
            var result = userManager.PasswordValidators[0].ValidateAsync(userManager, user, password);
            return result.Result;
        }
    }
}
