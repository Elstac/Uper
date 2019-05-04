namespace WebApp.Data.Specifications
{
    public class IncludeTestSpecification : BaseSpecification<TripDetails>
    {
        public IncludeTestSpecification() : base(null)
        {
            AddInclude(td => td.Passangers)
                .AddThenInclude(tu => tu.User);
        }
    }
}
