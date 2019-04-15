using System;
using System.Collections.Generic;

namespace WebApp.Data.Repositories
{
    public class MockupTripDetailsRepository:MockupBaseRepository<TripDetails>, ITripDetailsRepository
    {
        public MockupTripDetailsRepository()
        {
            dbContext = new List<TripDetails>()
            {
                new TripDetails(){
                    Id = 0,
                    Description = "First Uper trip",
                    DestinationAddress = new Address(){
                        City = "CityDes",
                        Street = "StreetDes"
                    },
                    StartingAddress = new Address(){
                        City = "CityStart",
                        Street = "StreetStart"
                    },
                    Cost = 100,
                    Date = DateTime.Now,
                    VechicleModel = "???",    
                    DriverId = "27562d79-9470-45ab-9181-ad8944b2b458",
                    Passangers = new List<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Id = "1",
                            Email ="pieciolot@gmail.com",
                            UserName = "Piecia"
                        }
                    }
                }
        };
        }
    }
}
