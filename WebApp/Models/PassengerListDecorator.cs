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

        public TripDetailsViewModel CreateViewModel(TripDetails tripDetails, ViewerType viewerType)
        {
            var vm = wrape.CreateViewModel(tripDetails, viewerType);

            if(viewerType != ViewerType.Guest)
            {
                vm.PassangersUsernames = (from user in tripDetails.Passangers
                                         select user.UserName).ToList();
            }

            return vm;
        }
        
    }
}
