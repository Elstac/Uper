namespace WebApp.Data.Entities
{
    public class TripUser:BaseEntity
    {
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TripId { get; set; }
        public TripDetails Trip { get; set; }

    }
}
