using System.Linq;
using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Models.TripDetailViewModelProvider
{
    public class RequestListDecorator : ITripDetailsCreator
    {
        private ITripDetailsCreator wrape;
        public RequestListDecorator(ITripDetailsCreator wrape)
        {
            this.wrape = wrape;
        }

        public TripDetailsViewModel CreateViewModel(TripDetails tripDetails)
        {
            var vm = wrape.CreateViewModel(tripDetails);

            vm.RequestsUsernames = new System.Collections.Generic.List<string>();
            vm.RequestsUsernames = (from username in tripDetails.Passangers
                                    where username.Accepted == false
                                    select username.User.UserName).ToList();

            return vm;
        }
    }
}
