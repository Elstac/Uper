using System;

namespace WebApp.Data.Entities
{
    public class RatesAndComment:BaseEntity
    {
        public int DrivingSafety { get; set; }
        public int PersonalCulture { get; set; }
        public int Punctuality { get; set; }
        public string Comment { get; set; }
        public string DriverId { get; set; }
        public string UserId { get; set; }
        public int TripId { get; set; }
        public DateTime Date { get; set; }
    }
}
