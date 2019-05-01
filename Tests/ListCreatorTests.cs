using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Models.OfferList;
using Xunit;

namespace Tests
{
    public class ListCreatorTests
    {
        [Fact]
        public void ListCreator_Test()
        {
            var repository = new Mock<ITripDetailsRepository>();
            List<TripDetails> testQuery = new List<TripDetails>();
            TripDetails trip = new TripDetails()
            {
                Cost = 100,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Moskwa" },
                Date = new DateTime(2019, 4, 20, 14, 30, 0)
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 324.86f,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Hong Kong" },
                Date = new DateTime(2019, 4, 20, 14, 30, 0)
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 250.00f,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "MosKwa" },
                Date = new DateTime(2019, 4, 20, 14, 30, 0)
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 210.50f,
                StartingAddress = new Address() { City = "kAIr" },
                DestinationAddress = new Address { City = "moSkWa" },
                Date = new DateTime(2019, 4, 20, 14, 30, 0)
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 205.65f,
                StartingAddress = new Address() { City = "Helsinki" },
                DestinationAddress = new Address { City = "Berlin" },
                Date = new DateTime(2019, 4, 20, 14, 30, 0)
            };
            testQuery.Add(trip);
            IEnumerable<TripDetails> testQuery_ = testQuery;

            repository
                .Setup(m=>m.GetAll())
                .Returns(testQuery_);
            ListCreator list = new ListCreator(repository.Object);
            IQueryable<TripDetails> travelList = list.GetList(210.50f, "Kair", "mosKwa",new DateTime(2019,4,4,14,30,0), new DateTime(2019, 5, 4, 14, 30, 0));

            Assert.Equal("Kair", travelList.First().StartingAddress.City);
            Assert.Equal("Moskwa", travelList.First().DestinationAddress.City);
            Assert.Equal("kAIr", travelList.Last().StartingAddress.City);
            Assert.Equal("moSkWa", travelList.Last().DestinationAddress.City);
            Assert.Equal(100, travelList.First().Cost);
            Assert.Equal(210.50f, travelList.Last().Cost);
            if (travelList.Count() == 2) { Assert.True(true); }
            else { Assert.True(false); }
        }

        [Fact]
        public void ListCreator_listIsEmpty()
        {
            var repository = new Mock<ITripDetailsRepository>();
            List<TripDetails> testQuery = new List<TripDetails>();
            TripDetails trip = new TripDetails()
            {
                Cost = 100,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Moskwa" },
                Date = new DateTime(2019, 4, 20, 14, 30, 0)
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 205.65f,
                StartingAddress = new Address() { City = "Helsinki" },
                DestinationAddress = new Address { City = "Berlin" },
                Date = new DateTime(2019, 4, 20, 14, 30, 0)
            };
            testQuery.Add(trip);
            IEnumerable<TripDetails> testQuery_ = testQuery;

            repository
                .Setup(m => m.GetAll())
                .Returns(testQuery_);
            ListCreator list = new ListCreator(repository.Object);

            IQueryable<TripDetails> travelList = list.GetList(1002.54f, "Moskwa", "Berlin", new DateTime(2019, 4, 4, 14, 30, 0), new DateTime(2019, 5, 4, 14, 30, 0));

            Assert.Empty(travelList);
        }

        [Fact]
        public void ListCreator_DateTimeTest()
        {
            var repository = new Mock<ITripDetailsRepository>();
            List<TripDetails> testQuery = new List<TripDetails>();
            TripDetails trip = new TripDetails()
            {
                Cost = 100,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Moskwa" },
                Date = new DateTime(2019, 12, 12, 4, 30, 11)
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 100,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Moskwa" },
                Date = new DateTime(2018, 4, 20, 14, 30, 0)
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 100,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Moskwa" },
                Date = new DateTime(2020, 5, 5, 6, 30, 0)
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 210.50f,
                StartingAddress = new Address() { City = "kAIr" },
                DestinationAddress = new Address { City = "moSkWa" },
                Date = new DateTime(2020, 5, 5, 5, 5, 5)
            };
            testQuery.Add(trip);
            trip = new TripDetails()
            {
                Cost = 100,
                StartingAddress = new Address() { City = "Kair" },
                DestinationAddress = new Address { City = "Moskwa" },
                Date = new DateTime(2020, 6, 20, 14, 30, 0)
            };
            testQuery.Add(trip);
            IEnumerable<TripDetails> testQuery_ = testQuery;

            repository
                .Setup(m => m.GetAll())
                .Returns(testQuery_);
            ListCreator list = new ListCreator(repository.Object);
            IQueryable<TripDetails> travelList = list.GetList(210.50f, "Kair", "mosKwa", new DateTime(2019, 10, 10, 10, 10, 10), new DateTime(2020, 5, 5, 5, 5, 5));

            Assert.Equal("Kair", travelList.First().StartingAddress.City);
            Assert.Equal("Moskwa", travelList.First().DestinationAddress.City);
            Assert.Equal("kAIr", travelList.Last().StartingAddress.City);
            Assert.Equal("moSkWa", travelList.Last().DestinationAddress.City);
            Assert.Equal(100, travelList.First().Cost);
            Assert.Equal(210.50f, travelList.Last().Cost);
            if (travelList.Count() == 2) { Assert.True(true); }
            else { Assert.True(false); }
        }
    }
}
