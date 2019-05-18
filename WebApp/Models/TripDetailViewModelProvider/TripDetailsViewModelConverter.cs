using System;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.Models.Factories;
using WebApp.ViewModels;

namespace WebApp.Models.TripDetailViewModelProvider
{
    public interface ITripDetailsViewModelConverter
    {
        List<TripDetailsViewModel> Convert(IEnumerable<TripDetails> dataModels, ViewerType type);
    }

    public class TripDetailsViewModelConverter : ITripDetailsViewModelConverter
    {
        private ITripDetailsViewModelCreatorFactory factory;

        public TripDetailsViewModelConverter(ITripDetailsViewModelCreatorFactory factory)
        {
            this.factory = factory;
        }

        public List<TripDetailsViewModel> Convert(IEnumerable<TripDetails> dataModels, ViewerType type)
        {
            var creator = factory.CreateCreator(type);

            foreach (var dataModel in dataModels)
            {
                creator.CreateViewModel(dataModel);
            }

            return null;
        }
    }
}
