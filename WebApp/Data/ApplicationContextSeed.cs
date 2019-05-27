using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Entities;

namespace WebApp.Data
{
    public class ApplicationContextSeed
    {
        public static void Seed(ApplicationContext context)
        {
            context.Database.Migrate();

            if (!context.TripDetails.Any())
            {
                context.AddRange(GetTrips());
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = GetUsers("Bob", "Klop", context.TripDetails.ToList());
                context.Users.AddRange(users);

                context.Users.Add(new ApplicationUser
                {
                    UserName = "PawelJumper",
                    Id = "dawajtojuzsiekameruje",
                    TripList = new List<TripUser>
                    {
                        new TripUser
                        {
                            Trip = new TripDetails
                            {
                                StartingAddress = new Address
                                {
                                    City = "1.5m"
                                },
                                DestinationAddress = new Address
                                {
                                    City = "gleba"
                                },
                                Cost = 0,
                                Date = DateTime.Now + TimeSpan.FromDays(3)+ TimeSpan.FromHours(3),
                                DateEnd = DateTime.Now + TimeSpan.FromDays(3)+ TimeSpan.FromHours(3)+TimeSpan.FromMinutes(1),
                                Description = "Ała kurcze rzeczywiście, kurcze, ała jak mnie wszystko boli",
                                IsSmokingAllowed = true,
                                Size = 4,
                                VechicleModel = "Bolid same szimanochy, stan idealny, lakierek nowy"
                            }
                        }
                    }
                });
                context.SaveChanges();
            }

            if(!context.RatesAndComment.Any())
            {
                var users = context.Users.ToList();
                context.AddRange(GetComments(users));
                context.SaveChanges();
            }
        }

        public static IEnumerable<TripDetails> GetTrips()
        {
            return new List<TripDetails>
            {
                new TripDetails
                {
                    StartingAddress = new Address
                    {
                        City = "Warszawa",
                        Street = "Nowogrodzka"
                    },
                    DestinationAddress = new Address
                    {
                        City = "Twierdza czarnoprochowa",
                        Street = "Kucowa"
                    },
                    Cost = 10,
                    Date = DateTime.Now + TimeSpan.FromDays(10),
                    DateEnd = DateTime.Now + TimeSpan.FromDays(12),
                    Description = "Szturm na ostatni bastion wolności",
                    IsSmokingAllowed = true,
                    Size = 4,
                    VechicleModel = "Tygrys"
                },
                new TripDetails
                {
                    StartingAddress = new Address
                    {
                        City = "Twierdza czarnoprochowa",
                        Street = "Kucowa"
                    },
                    DestinationAddress = new Address
                    {
                        City = "Moskwa",
                        Street = "?"
                    },
                    Cost = 15,
                    Date = DateTime.Now + TimeSpan.FromDays(8),
                    DateEnd = DateTime.Now + TimeSpan.FromDays(9),
                    Description = "Zdanie raportu przez agentow Moskwy",
                    IsSmokingAllowed = true,
                    Size = 15,
                    VechicleModel = "Passat 1.9 TDI"
                }
            };
        }

        public static IEnumerable<ApplicationUser> GetUsers(
            string baseName,
            string baseLastName,
            IEnumerable<TripDetails> tripList)
        {
            var users = new List<ApplicationUser>();

            for (int i = 0; i < tripList.Count(); i++)
            {
                users.Add(new ApplicationUser
                {
                    Id = i.ToString(),
                    Name = baseName,
                    Surname = baseLastName,
                    UserName = baseName + i,
                    Email = baseName+'.'+baseLastName+i+"@example.com",
                    TripList = new List<TripUser>
                    {
                        new TripUser
                        {
                            Trip = tripList.ToList()[i]
                        }
                    }
                });
            }

            return users;
        }

        private static IEnumerable<RatesAndComment> GetComments(IEnumerable<ApplicationUser> userList)
        {
            var ret = new List<RatesAndComment>();

            foreach (var user in userList)
            {
                ret.Add(new RatesAndComment
                {
                    Comment ="Super kierowca polecam",
                    DriverId = user.Id,
                    Punctuality = 5,
                    PersonalCulture = 5,
                    DrivingSafety = 5
                });
            }

            return ret;
        }
    }
}
