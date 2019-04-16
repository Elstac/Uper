namespace WebApp.Data.Entities
{
    public class TripUser:BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TripId { get; set; }
        public TripDetails Trip { get; set; }

    }
}
