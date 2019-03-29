using Xunit;
using Moq;
using WebApp.Models;
using WebApp.Data.Repositories;
using WebApp.ViewModels;
using WebApp.Data;
using System;

namespace Tests
{
    public class TripDetailsViewModelGeneratorTests
    {
        private TripDetailsViewModelGenerator test;
        private TripDetails trip;
        public TripDetailsViewModelGeneratorTests()
        {
            trip = new TripDetails() { Id=1,
                Cost = 10,
                Description = "des",
                DriverId =10,
                VechicleModel ="model",
                Date = new DateTime(1212,12,12),
                };

            Mock<ITripDetailsRepository> repoMoq = new Mock<ITripDetailsRepository>();
            repoMoq.Setup(e=>e.GetById(1)).Returns(trip);
            test = new TripDetailsViewModelGenerator(repoMoq.Object);
        }

        [Fact]
        public void ReturnMinimalViewModelIfViewerIsGuest()
        {
            var vm = test.GetViewModel(1, ViewerType.Guest);

            Assert.Equal(trip.Description, vm.Description);
            Assert.Equal(trip.VechicleModel, vm.VechicleModel);
            Assert.Equal(trip.Date, vm.Date);
            Assert.Equal(trip.Cost, vm.Cost);
            Assert.Null(vm.PassangersUsernames);
        }
    }
}
