namespace WebApp.Data
{
    public class TripDetails:BaseEntity
    {
        public Address DestinationAddress { get; set; }
        public Address StartingAddress { get; set; }
        public int DriverId { get; set; }
        public float Cost { get; set; }
    }
}
