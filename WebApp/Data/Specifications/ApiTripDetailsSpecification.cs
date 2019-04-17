namespace WebApp.Data.Specifications
{
    /// <summary>
    /// Query returns 20 latest trips
    /// </summary>
    public class ApiTripDetailsSpecification : BaseSpecification<TripDetails>
    {
        public ApiTripDetailsSpecification():base(
            td => td.Size < td.Passangers.Count)
        {
            ApplyOrderBy(td => td.Date);
            ApplyPageing(20, 0);
        }
    }
}
