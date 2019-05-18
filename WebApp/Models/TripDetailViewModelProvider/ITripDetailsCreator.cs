using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public interface ITripDetailsCreator
    {
        TripDetailsViewModel CreateViewModel(TripDetails tripDetails);
    }

    public class TripDetailsCreator : ITripDetailsCreator
    {
        private IApplicationUserRepository repository;

        public TripDetailsCreator(IApplicationUserRepository repository)
        {
            this.repository = repository;
        }

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
                StartingAddress = tripDetails.StartingAddress,
                DriverId = tripDetails.DriverId,
                DriverUsername = repository.GetById(tripDetails.DriverId).UserName
            };

            return ret;
        }
    }
}
