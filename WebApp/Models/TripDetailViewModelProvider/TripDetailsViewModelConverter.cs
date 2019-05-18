using System;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Models.TripDetailViewModelProvider
{
    public interface ITripDetailsViewModelConverter
    {
        List<TripDetailsViewModel> Convert(IEnumerable<TripDetails> dataModels);
    }

    public class TripDetailsViewModelConverter : ITripDetailsViewModelConverter
    {
        public List<TripDetailsViewModel> Convert(IEnumerable<TripDetails> dataModels)
        {
            throw new NotImplementedException();
        }
    }
}
