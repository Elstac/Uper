using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public interface IApplicationUserViewModelGenerator
    {
        ApplicationUserViewModel ConvertAppUserToViewModel(ApplicationUser AppUser);
    }

    public class ApplicationUserViewModelGenerator : IApplicationUserViewModelGenerator
    {

        public ApplicationUserViewModelGenerator()
        {
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