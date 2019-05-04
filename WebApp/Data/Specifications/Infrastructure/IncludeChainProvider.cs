namespace WebApp.Data.Specifications.Infrastructure
{
    public interface IIncludeChainProvider
    {
        IIncluder GetIncluder();
    }

    class IncludeChainProvider : IIncludeChainProvider
    {
        public IIncluder GetIncluder()
        {
            return new TripUserCollectionIncluder(
                new TripUserCollectionIncluder(
                    new UserIncluder(null)));
        }
    }
}