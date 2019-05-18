using Xunit;
using Moq;
using WebApp.Models.TripDetailViewModelProvider;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.Models;
using System;
using WebApp.Models.Factories;
using WebApp.ViewModels;

namespace Tests.TripDetailsViewModelGenerationTests.TripDetailsViewModelConverterTests
{
    public class ConvertListTests
    {
        private TripDetailsViewModelConverter converter;
        private Mock<ITripDetailsViewModelCreatorFactory> facMock;
        private Mock<ITripDetailsCreator> creatorMock;

        public ConvertListTests()
        {
            creatorMock = new Mock<ITripDetailsCreator>();

            InitializeFacMock();

            converter = new TripDetailsViewModelConverter(facMock.Object);
        }


        private void InitializeFacMock()
        {
            facMock = new Mock<ITripDetailsViewModelCreatorFactory>();
            facMock.Setup(fm => fm.CreateCreator(It.IsAny<ViewerType>())).Returns(creatorMock.Object);
        }

        [Fact]
        public void GetCreatorFromFactoryUsingGivenViewerType()
        {
            converter.Convert(new List<TripDetails>(), ViewerType.Driver);

            facMock.Verify(fm => fm.CreateCreator(ViewerType.Driver));
        }

        [Fact]
        public void PassAllDataModelInGivenListToCreator()
        {
            var inputList = new List<TripDetails>
                {
                    new TripDetails(),
                    new TripDetails(),
                    new TripDetails(),
                    new TripDetails()
                };

            converter.Convert(
                inputList,
                ViewerType.Driver
            );

            foreach (var item in inputList)
            {
                creatorMock.Verify(cm => cm.CreateViewModel(item));
            }
        }

        [Fact]
        public void AddCreatedViewModelsToListAdneReturnIt()
        {
            var vm = new TripDetailsViewModel();
            creatorMock = new Mock<ITripDetailsCreator>();
            creatorMock.Setup(cm => cm.CreateViewModel(It.IsAny<TripDetails>()))
                .Returns(vm);

            InitializeFacMock();

            converter = new TripDetailsViewModelConverter(facMock.Object);

            var inputList = new List<TripDetails>
                {
                    new TripDetails(),
                    new TripDetails(),
                    new TripDetails(),
                    new TripDetails()
                };

            var @out = converter.Convert(
                inputList,
                ViewerType.Driver
            );

            foreach (var item in @out)
            {
                Assert.Equal(vm,item);
            }
        }

    }
}
