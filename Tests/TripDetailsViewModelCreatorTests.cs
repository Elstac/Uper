using System;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Models;
using Xunit;
using Moq;
using WebApp.Data.Repositories;

namespace Tests
{
    public class TripDetailsViewModelCreatorTests
    {
        private ITripDetailsCreator detailsCreator;
        private Mock<IApplicationUserRepository> repoMock;

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
                        Accepted = true
                    },
                    new TripUser
                    {
                        User = new ApplicationUser
                        {
                            UserName = "PieciaNotAccepted"
                        },
                        Accepted = false
                    }
                },
                Description = "des",
                DestinationAddress = new Address { City = "city", Street = "street" },
                StartingAddress = new Address { City = "city", Street = "street" },
                MapId = "1"
            };
            repoMock = new Mock<IApplicationUserRepository>();
            repoMock.Setup(rm => rm.GetById(It.IsAny<string>()))
                .Returns(new ApplicationUser
                {
                    UserName = "Jan"
                });

            detailsCreator = new TripDetailsCreator(repoMock.Object);
        }

        [Fact]
        public void ReturnMinimalViewModelForBaseCreator()
        {
            var vm = detailsCreator.CreateViewModel(testModel);

            Assert.Equal(testModel.Description, vm.Description);
            Assert.Equal(testModel.Date, vm.Date);
            Assert.Equal(testModel.DestinationAddress, vm.DestinationAddress);
            Assert.Equal(testModel.StartingAddress, vm.StartingAddress);
            Assert.Equal(testModel.Cost, vm.Cost);
            Assert.Equal("Jan",vm.DriverUsername);
            Assert.Null(vm.PassangersUsernames);
        }

        [Fact]
        public void ReturnViewModelWithConfirmedPassangersListWhenUsedPassengerListDecorator()
        {
            detailsCreator = new PassengerListDecorator(detailsCreator);
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

        [Fact]
        public void ReturnViewModelWithMapWhenUsedMapDecorator()
        {
            detailsCreator = new MapDecorator(detailsCreator);

            var vm = detailsCreator.CreateViewModel(testModel);

            Assert.Equal("/images/maps/1.json",vm.MapPath);
        }
    }
}
