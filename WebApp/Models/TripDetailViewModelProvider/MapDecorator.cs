using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public class MapDecorator : ITripDetailsCreator
    {
        ITripDetailsCreator wrape;

        public MapDecorator(ITripDetailsCreator wrape)
        {
            this.wrape = wrape;
        }

        public TripDetailsViewModel CreateViewModel(TripDetails tripDetails)
        {
            var vm = wrape.CreateViewModel(tripDetails);

            if (tripDetails.MapId != null)
                vm.MapPath = "/images/maps" + tripDetails.MapId + ".png";
            else
                vm.MapPath = "/images/nomap.png";

            return vm;
        }
    }
}
