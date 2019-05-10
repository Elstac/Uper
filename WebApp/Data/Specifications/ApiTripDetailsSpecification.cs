namespace WebApp.Data.Specifications
{
    /// <summary>
    /// Query returns x latest trips
    /// </summary>
    public class ApiTripDetailsSpecification : BaseSpecification<TripDetails>
    {
        public ApiTripDetailsSpecification(int take):base(
            null)
        {
            ApplyOrderBy(td => td.Date);
            ApplyPageing(take, 0);
        }
    }
}
