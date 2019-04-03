using WebApp.Data;
using WebApp.ViewModels;
using System.Linq;

namespace WebApp.Models
{
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
            
            vm.PassangersUsernames = (from user in tripDetails.Passangers
                                      select user.Name).ToList();

            return vm;
        }
        
    }
}
