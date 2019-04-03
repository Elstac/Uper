using System;
using System.Collections.Generic;
using WebApp.Data;
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
                Passangers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Name = "Piecia"
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
        public void ReturnViewModelWithPassangersListWhenUsedPassengerListDecorator()
        {
            detailsCreator = new PassengerListDecorator(new TripDetailsCreator());
            var vm = detailsCreator.CreateViewModel(testModel);

            Assert.Equal(testModel.Description, vm.Description);
            Assert.Equal(testModel.Date, vm.Date);
            Assert.Equal(testModel.DestinationAddress, vm.DestinationAddress);
            Assert.Equal(testModel.StartingAddress, vm.StartingAddress);
            Assert.Equal(testModel.Cost, vm.Cost);

            int i = 0;
            foreach (var user in testModel.Passangers)
            {
                Assert.Equal(user.Name, vm.PassangersUsernames[i++]);
            }
        }
    }
}
