using System;
using WebApp.Data.Repositories;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public interface ITripDetailsViewModelGenerator
    {
        TripDetailsViewModel GetViewModel(int tripId, int type);
    }
    public class TripDetailsViewModelGenerator : ITripDetailsViewModelGenerator
    {
        private ITripDetailsRepository detailsRepository;

        public TripDetailsViewModelGenerator(ITripDetailsRepository detailsRepository)
        {
            this.detailsRepository = detailsRepository;
        }

        public TripDetailsViewModel GetViewModel(int tripId, int type)
        {
            throw new NotImplementedException();
        }
    }
}
