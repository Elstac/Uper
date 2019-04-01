using System;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public interface IApplicationUserViewModelGenerator
    {
        ApplicationUserViewModel GetViewModel(int tripId);
    }
    public class ApplicationUserViewModelGenerator : IApplicationUserViewModelGenerator
    {
        private IApplicationUserRepository<ApplicationUser> userRepository;

        public ApplicationUserViewModelGenerator(IApplicationUserRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;

        }

        public ApplicationUserViewModel GetViewModel(int tripId)
        {
            var dataModel = userRepository.GetById(tripId.ToString());

            var ret = new ApplicationUserViewModel
            {
                UserName = dataModel.UserName,
                Name = dataModel.UserName,
                Surname = dataModel.Surname,
                Email = dataModel.Email,
                PhoneNumber = dataModel.PhoneNumber,
                Rating = dataModel.Rating,
                NumOfVotes = dataModel.NumOfVote
            };
            return ret;
        }
    }
}