using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Data.Entities;

namespace WebApp.Data
{
    public class TripDetails:BaseEntity
    {
        public Address DestinationAddress { get; set; }
        public Address StartingAddress { get; set; }
        public string DriverId { get; set; }
        public float Cost { get; set; }
        public string Description { get; set; }
        public string VechicleModel { get; set; }
        public int Size { get; set; }
        public DateTime Date { get; set; }

        public ICollection<TripUser> Passangers { get; set; }
    }
}