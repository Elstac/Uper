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
                    Description = "XDD",
                    DestinationAddress = new Address(){
                        City = "Dziura w dupie",
                        Street = "prokurator"
                    }
                }
        };
        }
    }
}
