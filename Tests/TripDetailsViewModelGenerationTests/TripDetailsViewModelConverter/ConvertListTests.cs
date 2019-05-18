using Xunit;
using Moq;
using WebApp.Models.TripDetailViewModelProvider;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.Models;
using System;
using WebApp.Models.Factories;

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

            facMock = new Mock<ITripDetailsViewModelCreatorFactory>();
            facMock.Setup(fm => fm.CreateCreator(It.IsAny<ViewerType>())).Returns(creatorMock.Object);

            converter = new TripDetailsViewModelConverter(facMock.Object);
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

            var @out = converter.Convert(
                inputList,
                ViewerType.Driver
            );

            foreach (var item in inputList)
            {
                creatorMock.Verify(cm => cm.CreateViewModel(item));
            }
        }
    }
}
