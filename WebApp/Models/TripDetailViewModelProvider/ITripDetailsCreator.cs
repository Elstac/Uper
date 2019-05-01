using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public interface ITripDetailsCreator
    {
        TripDetailsViewModel CreateViewModel(TripDetails tripDetails);
    }

    public class TripDetailsCreator : ITripDetailsCreator
    {
        public TripDetailsViewModel CreateViewModel(TripDetails tripDetails)
        {
            var ret = new TripDetailsViewModel
            {
                Cost = tripDetails.Cost,
                Description = tripDetails.Description,
                VechicleModel = tripDetails.VechicleModel,
                Date = tripDetails.Date,
                DestinationAddress = tripDetails.DestinationAddress,
                StartingAddress = tripDetails.StartingAddress,
                MapPath = "/images/maps/" + tripDetails.MapId + ".png"
            };

            return ret;
        }
    }
}
