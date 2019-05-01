using System;
using System.Collections.Generic;
using WebApp.Data;
namespace WebApp.ViewModels
{
    public class TripDetailsViewModel
    {
        public Address DestinationAddress { get; set; }
        public Address StartingAddress { get; set; }
        public float Cost { get; set; }
        public string Description { get; set; }
        public string VechicleModel { get; set; }
        public DateTime Date { get; set; }
        public string DriverUsername { get; set; }
        public int Size { get; set; }
        public string MapPath { get; set; }
        public List<string> PassangersUsernames { get; set; }
        
    }
}
