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
            var dataModel = detailsRepository.GetById(tripId);

            var ret = new TripDetailsViewModel
            {
                Cost = dataModel.Cost,
                Description = dataModel.Description,
                VechicleModel = dataModel.VechicleModel,
                Date = dataModel.Date,
                DestinationAddress = dataModel.DestinationAddress,
                StartingAddress = dataModel.StartingAddress
            };

            switch (type)
            {
                case ViewerType.Passanger:
                    break;
                case ViewerType.Driver:
                    break;
            }
            
            return ret;
        }
    }
}
