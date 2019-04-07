using System;
using System.Collections.Generic;

namespace WebApp.Data.Repositories
{
    public class TripDetailsRepository:MockupBaseRepository<TripDetails>, ITripDetailsRepository
    {
        public TripDetailsRepository()
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
                    DriverId = 1,
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
