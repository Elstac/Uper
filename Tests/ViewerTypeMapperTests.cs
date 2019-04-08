using System.Collections.Generic;
using WebApp.Data;
using WebApp.Models;
using Xunit;

namespace Tests
{
    public class ViewerTypeMapperTests
    {
        private ViewerTypeMapper typeMapper;

        public ViewerTypeMapperTests()
        {
            typeMapper = new ViewerTypeMapper();
        } 

        [Fact]
        public void ReturnGuestIfUserIsNotDriverOrPassanger()
        {
            var output = typeMapper.GetViewerType(
                new ApplicationUser
                {
                    Id = "1"
                },
                new TripDetails
                {
                    DriverId = "2",
                    Passangers = null
                });

            Assert.Equal(ViewerType.Guest, output);
        }

        [Fact]
        public void ReturnGuestIfUserIsNull()
        {
            var output = typeMapper.GetViewerType(
                null,
                new TripDetails
                {
                    DriverId = "2",
                    Passangers = null
                });

            Assert.Equal(ViewerType.Guest, output);
        }

        [Fact]
        public void ReturnPassangerIfUserIsInPassangerList()
        {
            var user = new ApplicationUser
            {
                Id = "1"
            };

            var output = typeMapper.GetViewerType(user,
                new TripDetails
                {
                    DriverId = "2",
                    Passangers = new List<ApplicationUser>
                    {
                        user
                    }
                });

            Assert.Equal(ViewerType.Passanger, output);
        }

        [Fact]
        public void ReturnDriverIfUserHasDriverid()
        {
            var user = new ApplicationUser
            {
                Id = "1"
            };

            var output = typeMapper.GetViewerType(user,
                new TripDetails
                {
                    DriverId = "1",
                    Passangers = null
                });

            Assert.Equal(ViewerType.Driver, output);
        }
    }
}
