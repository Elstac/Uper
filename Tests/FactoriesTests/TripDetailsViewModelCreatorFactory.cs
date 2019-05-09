using WebApp.Models.Factories;
using Xunit;
using Moq;
using System;
using WebApp.Models;
using System.Reflection;
using Xunit.Sdk;

namespace Tests.FactoriesTests
{
    public class TripDetailsViewModelCreatorFactory
    {
        TripDetailViewModelCreatorFactory viewModelCreatorFactory;

        //[Fact]
        //public void ReturnBaseCreatorIfUserIsGuest()
        //{
        //    var spMock = new Mock<IServiceProvider>();
        //    spMock.Setup(sm => sm.GetService(typeof(ITripDetailsCreator)))
        //        .Returns(new TripDetailsCreator());

        //    viewModelCreatorFactory = new TripDetailViewModelCreatorFactory(spMock.Object);

        //    var creator = viewModelCreatorFactory.CreateCreator(ViewerType.Guest);

        //    Assert.True(creator.GetType() == typeof(TripDetailsCreator));
        //}

        //[Theory]
        //[InlineData(ViewerType.Passanger)]
        //[InlineData(ViewerType.Driver)]
        //public void ReturnBaseCreatorWrapedWithPasssangerListDecoratorIfUserIsNotGuest(ViewerType viewerType)
        //{
        //    var spMock = new Mock<IServiceProvider>();
        //    spMock.Setup(sm => sm.GetService(typeof(ITripDetailsCreator)))
        //        .Returns(new TripDetailsCreator());

        //    viewModelCreatorFactory = new TripDetailViewModelCreatorFactory(spMock.Object);

        //    var creator = viewModelCreatorFactory.CreateCreator(viewerType);
        //}

        //[Fact]
        //public void ReturnBaseCreatorWrapedWithPasssangerListDecorator()
        //{
        //    var spMock = new Mock<IServiceProvider>();
        //    spMock.Setup(sm => sm.GetService(typeof(ITripDetailsCreator)))
        //        .Returns(new TripDetailsCreator());

        //    viewModelCreatorFactory = new TripDetailViewModelCreatorFactory(spMock.Object);

        //    var creator = viewModelCreatorFactory.CreateCreator(ViewerType.Guest);

        //    Assert.True(creator.GetType() == typeof(TripDetailsCreator));
        //}

    }
}
