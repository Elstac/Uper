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
            
<<<<<<< HEAD
            vm.PassangersUsernames = (from tu in tripDetails.Passangers
                                      select tu.User.UserName).ToList();
=======
            vm.PassangersUsernames = (from tu in tripDetails.Passangers select tu.User.Name).ToList();
>>>>>>> Added Joining and leaving Trips by Users

            return vm;
        }
    }
}
