using System;
using System.Collections.Generic;

namespace WebApp.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private ApplicationContext context;

        public ApplicationUserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(ApplicationUser toAdd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(string id)
        {
            return context.Set<ApplicationUser>().Find(id);
        }

        public IEnumerable<ApplicationUser> GetList(ISpecification<ApplicationUser> specification)
        {
            throw new NotImplementedException();
        }

        public void Remove(ApplicationUser toRemove)
        {
            throw new NotImplementedException();
        }
        //public ApplicationUserRepository()
        //{

        //    dbContext = new System.Collections.Generic.List<ApplicationUser>()
        //    {
        //        new ApplicationUser()
        //        {
        //            Id = "1",
        //            UserName = "janex123",
        //            Name = "Jan",
        //            Surname = "Kolumb",
        //            Rating = 5,
        //            NumOfVote = 1,
        //            Email = "jantoja@wp.pl",
        //            EmailConfirmed = true,
        //            PhoneNumber = "514665123",
        //            Role = 0,
        //            PasswordHash = "ToHasloAleShashowane",
        //            Description = "hi im Jan and im a very happy not creepy driver",
        //            TripList = new List<TripDetails>()
        //            {
        //                new TripDetails()
        //                {
        //                    Id = 1,
        //                    DriverId = "1"
        //                }
        //            }
        //        }
        //    };
        //}
    }
}
