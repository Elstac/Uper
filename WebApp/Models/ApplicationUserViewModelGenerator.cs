using Microsoft.AspNetCore.Identity;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public interface IApplicationUserViewModelGenerator
    {
        ApplicationUserViewModel ConvertAppUserToViewModel(ApplicationUser AppUser);
    }

    public class ApplicationUserViewModelGenerator : IApplicationUserViewModelGenerator
    {
        private IAccountManager accountManager;
        private IApplicationUserRepository userRepository;
        private UserManager<ApplicationUser> userManager;

        public ApplicationUserViewModelGenerator(IApplicationUserRepository userRepository,IAccountManager accountManager, UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.accountManager = accountManager;
            this.userManager = userManager;
        }

        public ApplicationUserViewModel ConvertAppUserToViewModel(ApplicationUser AppUser)
        {
            var ret = new ApplicationUserViewModel
            {
                UserName = AppUser.UserName,
                Name = AppUser.Name,
                Surname = AppUser.Surname,
                Email = AppUser.Email,
                PhoneNumber = AppUser.PhoneNumber,
                Rating = AppUser.Rating,
                NumOfVotes = AppUser.NumOfVote,
                Description = AppUser.Description,
                Confirmed = AppUser.EmailConfirmed
            };
            return ret;
        }
    }
}