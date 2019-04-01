using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public interface ITripDetailsCreator
    {
        TripDetailsViewModel CreateViewModel(TripDetails tripDetails);
    }

    public class TripDrtailsCreator : ITripDetailsCreator
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
                StartingAddress = tripDetails.StartingAddress
            };

            return ret;
        }
    }
}
