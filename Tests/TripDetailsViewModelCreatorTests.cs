using System;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Models;
using Xunit;

namespace Tests
{
    public class TripDetailsViewModelCreatorTests
    {
        private ITripDetailsCreator detailsCreator;
        private TripDetails testModel;

        public TripDetailsViewModelCreatorTests()
        {

            testModel = new TripDetails
            {
                Cost = 10,
                Date = DateTime.Today,
                Passangers = new List<TripUser>
                {
                    new TripUser
                    {
                        User = new ApplicationUser
                        {
                            UserName = "Piecia"
                        },
                        Accepted =true
                    },
                    new TripUser
                    {
                        User = new ApplicationUser
                        {
                            UserName ="PieciaNotAccepted"
                        },
                        Accepted = false
                    }
                },
                Description = "des",
                DestinationAddress = new Address { City = "city", Street = "street" },
                StartingAddress = new Address { City = "city", Street = "street" }
            };
        }

        [Fact]
        public void ReturnMinimalViewModelForBaseCreator()
        {
            detailsCreator = new TripDetailsCreator();
            var vm = detailsCreator.CreateViewModel(testModel);

            Assert.Equal(testModel.Description, vm.Description);
            Assert.Equal(testModel.Date, vm.Date);
            Assert.Equal(testModel.DestinationAddress, vm.DestinationAddress);
            Assert.Equal(testModel.StartingAddress, vm.StartingAddress);
            Assert.Equal(testModel.Cost, vm.Cost);
            Assert.Null(vm.PassangersUsernames);
        }

        [Fact]
        public void ReturnViewModelWithConfirmedPassangersListWhenUsedPassengerListDecorator()
        {
            detailsCreator = new PassengerListDecorator(new TripDetailsCreator());
            var vm = detailsCreator.CreateViewModel(testModel);

            Assert.Equal(testModel.Description, vm.Description);
            Assert.Equal(testModel.Date, vm.Date);
            Assert.Equal(testModel.DestinationAddress, vm.DestinationAddress);
            Assert.Equal(testModel.StartingAddress, vm.StartingAddress);
            Assert.Equal(testModel.Cost, vm.Cost);

            int i = 0;
            foreach (var userName in new string []{ "Piecia" })
            {
                Assert.Equal(userName, vm.PassangersUsernames[i++]);
            }
        }
    }
}
