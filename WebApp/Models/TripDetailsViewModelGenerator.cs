using System;
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

    public interface ITripDetailsViewModelGenerator
    {
        TripDetailsViewModel GetViewModel(int tripId, ViewerType type);
    }
    public class TripDetailsViewModelGenerator : ITripDetailsViewModelGenerator
    {
        private ITripDetailsRepository detailsRepository;

        public TripDetailsViewModelGenerator(ITripDetailsRepository detailsRepository)
        {
            this.detailsRepository = detailsRepository;
        }

        public TripDetailsViewModel GetViewModel(int tripId, ViewerType type)
        {
            throw new NotImplementedException();
        }
    }
}
