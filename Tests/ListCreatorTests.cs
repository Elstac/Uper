using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data;
using WebApp.Models.OfferList;
using Xunit;

namespace Tests
{
    public class ListCreatorTests
    {
        [Fact]
        public void ListCreator_Test()
        {
            var repository = new Mock<IRepository<TripDetails,int>>();
            List<TripDetails> testQuery = new List<TripDetails>();
            TripDetails trip = new TripDetails()
            {
                Cost = 100,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Moskwa" }
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 210.50f,
                StartingAddress = new Address() { City = "kAIr" },
                DestinationAddress = new Address { City = "moSkWa" }
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 324.86f,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Hong Kong" }
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 250.00f,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "MosKwa" }
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 205.65f,
                StartingAddress = new Address() { City = "Helsinki" },
                DestinationAddress = new Address { City = "Berlin" }
            };
            testQuery.Add(trip);
            IEnumerable<TripDetails> testQuery_ = testQuery;

            repository
                .Setup(m=>m.GetAll())
                .Returns(testQuery_);
            ListCreator list = new ListCreator(repository.Object);

            IQueryable<TripDetails> travelList = list.GetList(210.50f, "Kair", "MoSkwa");
            
            List<TripDetails> modelTravelList_ = new List<TripDetails>();
            trip = new TripDetails()
            {
                Cost = 100,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Moskwa" }
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 210.50f,
                StartingAddress = new Address() { City = "kAIr" },
                DestinationAddress = new Address { City = "moSkWa" }
            };
            testQuery.Add(trip);
            IEnumerable<TripDetails> modelTravelList__ = modelTravelList_;
            IQueryable<TripDetails> modelTravelList = modelTravelList__.AsQueryable();

            Assert.Equal(modelTravelList, travelList);
        }

        [Fact]
        public void ListCreator_listIsEmpty()
        {
            var repository = new Mock<IRepository<TripDetails, int>>();
            List<TripDetails> testQuery = new List<TripDetails>();
            TripDetails trip = new TripDetails()
            {
                Cost = 100,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Moskwa" }
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 205.65f,
                StartingAddress = new Address() { City = "Helsinki" },
                DestinationAddress = new Address { City = "Berlin" }
            };
            testQuery.Add(trip);
            IEnumerable<TripDetails> testQuery_ = testQuery;

            repository
                .Setup(m => m.GetAll())
                .Returns(testQuery_);
            ListCreator list = new ListCreator(repository.Object);

            IQueryable<TripDetails> travelList = list.GetList(1002.54f, "Moskwa", "Berlin");

            Assert.Empty(travelList);
        }
    }
}
