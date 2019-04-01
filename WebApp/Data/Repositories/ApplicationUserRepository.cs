using System;

namespace WebApp.Data.Repositories
{
    public class ApplicationUserRepository : MockupApplicationUserRepository<ApplicationUser>, IApplicationUserRepository<ApplicationUser>
    {
        public ApplicationUserRepository()
        {
            dbContext = new System.Collections.Generic.List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1",
                    UserName = "janex123",
                    Name = "Jan",
                    Surname = "Kolumb",
                    Rating = 5,
                    NumOfVote = 1,
                    Email = "jantoja@wp.pl",
                    EmailConfirmed = true,
                    PhoneNumber = "514665123",
                    Role = 0,
                    PasswordHash = "ToHasloAleShashowane"
                }
            };
        }
    }
}
