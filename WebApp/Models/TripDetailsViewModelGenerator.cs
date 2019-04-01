using WebApp.Data.Repositories;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public enum ViewerType
    {
        Guest,
        Passanger,
        Driver
    }

    /// <summary>
    /// Provides logic for creating WebApp.ViewModels.TripDetailViewModel.
    /// </summary>
    public interface ITripDetailsViewModelGenerator
    {
        /// <summary>
        /// Creates TripDetailViewModel depends on <paramref name="viewerType"/> based on DetailOffer with ID = <paramref name="tripId"/>
        /// </summary>
        /// <param name="tripId">TripDetail dataclass id</param>
        /// <param name="viewerType">Type of viewer</param>
        /// <returns></returns>
        TripDetailsViewModel GetViewModel(int tripId, ViewerType viewerType, ITripDetailsCreator creator);
    }

    /// <summary>
    /// Standard implementation of WebApp.Models.ITripDetailsViewModelGenerator
    /// </summary>
    public class TripDetailsViewModelGenerator : ITripDetailsViewModelGenerator
    {
        private ITripDetailsRepository detailsRepository;
        
        public TripDetailsViewModelGenerator(ITripDetailsRepository detailsRepository)
        {
            this.detailsRepository = detailsRepository;
        }

        /// <summary>
        /// Creates minimal ViewModel for site guests, extended for passangers and full for driver
        /// </summary>
        /// <param name="tripId">TripDetail id</param>
        /// <param name="viewerType">Type of viewer</param>
        /// <returns>viewmodel</returns>
        public TripDetailsViewModel GetViewModel(int tripId, ViewerType viewerType,ITripDetailsCreator creator)
        {
            var dataModel = detailsRepository.GetById(tripId);

            if (viewerType != ViewerType.Guest)
                creator = new PassengerListDecorator(creator);
            
            return creator.CreateViewModel(dataModel);
        }
    }
}
