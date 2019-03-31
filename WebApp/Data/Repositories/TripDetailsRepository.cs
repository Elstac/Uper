using System;

namespace WebApp.Data.Repositories
{
    public class TripDetailsRepository:MockupBaseRepository<TripDetails>, ITripDetailsRepository
    {
        public TripDetailsRepository()
        {
            dbContext = new System.Collections.Generic.List<TripDetails>()
            {
                new TripDetails(){
                    Id =1,
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
                    DriverId = 1
                }
        };
        }
    }
}
