using WebApp.Data;
using WebApp.ViewModels;
using System.Linq;
using WebApp.Data.Entities;
using WebApp.Data.Repositories;

namespace WebApp.Models
{
    /// <summary>
    /// Adds passangers usernames list to base TripDetailsViewModel.
    /// </summary>
    public class PassengerListDecorator : ITripDetailsCreator
    {
        private ITripDetailsCreator wrape;

        public PassengerListDecorator(ITripDetailsCreator wrape)
        {
            this.wrape = wrape;
        }

        public TripDetailsViewModel CreateViewModel(TripDetails tripDetails)
        {
            var vm = wrape.CreateViewModel(tripDetails);

            //version that work for others but not for me
            /*vm.PassangersUsernames = (from tu in tripDetails.Passangers
                                      select tu.User.UserName).ToList();
         */
            //My worse but working version , get userid a then in controller convert it into username
             vm.PassangersUsernames = (from tu in tripDetails.Passangers
                                      select tu.UserId).ToList();


            return vm;
        }
    }
}
