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
                TripId = tripDetails.Id,
                Size = tripDetails.Size,
                Cost = tripDetails.Cost,
                Description = tripDetails.Description,
                VechicleModel = tripDetails.VechicleModel,
                Date = tripDetails.Date,
                DateEnd = tripDetails.DateEnd,
                IsSmokingAllowed = tripDetails.IsSmokingAllowed,
                DestinationAddress = tripDetails.DestinationAddress,
                StartingAddress = tripDetails.StartingAddress
            };

            return ret;
        }
    }
}
