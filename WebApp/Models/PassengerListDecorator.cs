using WebApp.Data;
using WebApp.ViewModels;

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
                //TODO: gnerate list of pssangers and add it to viewmodel
            }

            return vm;
        }
        
    }
}
