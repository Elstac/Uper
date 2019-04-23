using WebApp.Data;
using WebApp.ViewModels;
using System.Linq;

namespace WebApp.Models
{
    /// <summary>
    /// Adds passangers usernames list to base TripDetailsViewModel.
    /// </summary>
    public class PassengerListDecorator : ITripDetailsCreator
    {
        private ITripDetailsCreator wrape;

        public PassengerListDecorator(ITripDetailsCreator wrape)
        {
            this.wrape = wrape;
        }

        public TripDetailsViewModel CreateViewModel(TripDetails tripDetails)
        {
            var vm = wrape.CreateViewModel(tripDetails);
            
            vm.PassangersUsernames = (from tu in tripDetails.Passangers select tu.User.UserName).ToList();

            return vm;
        }
    }
}
