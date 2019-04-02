using Xunit;
using Moq;
using WebApp.Models;
using WebApp.Data.Repositories;
using WebApp.Data;
using System;

namespace Tests
{
    public class TripDetailsViewModelGeneratorTests
    {
        private TripDetailsViewModelGenerator viewModelGenerator;
        private Mock<ITripDetailsCreator> creatorMock;
        private Mock<ITripDetailsRepository> repoMock;
        private readonly TripDetails testModel;

        public TripDetailsViewModelGeneratorTests()
        {
            creatorMock = new Mock<ITripDetailsCreator>();
            repoMock = new Mock<ITripDetailsRepository>();

            repoMock.Setup(e => e.GetById(4)).Returns(() => throw new IndexOutOfRangeException());

            testModel = new TripDetails();

            repoMock.Setup(e => e.GetById(1)).Returns(testModel);

            viewModelGenerator = new TripDetailsViewModelGenerator(repoMock.Object, creatorMock.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetDataModelWithGivemIdFromRepository(int type)
        {
            viewModelGenerator.GetViewModel(1, (ViewerType)type);

            repoMock.Verify(rep => rep.GetById(1),Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetViewModelWithGivemIdAndViewerTypeFromCreator(int type)
        {
            viewModelGenerator.GetViewModel(1, (ViewerType)type);

            creatorMock.Verify(rep => rep.CreateViewModel(testModel, (ViewerType)type), Times.Once);
        }

        [Theory]
        [InlineData(-1,0)]
        [InlineData(-1,1)]
        [InlineData(-1,2)]
        [InlineData(4,0)]
        [InlineData(4,1)]
        [InlineData(4,2)]
        public void ThrowIndexOutOfRangeExceptionWhenIdIsInvalid(int id,int type)
        {
            Assert.Throws<IndexOutOfRangeException>(() => viewModelGenerator.GetViewModel(id, (ViewerType)type));
        }
    }
}
