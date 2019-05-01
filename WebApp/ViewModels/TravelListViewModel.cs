using System;
using System.Linq;
using WebApp.Data;

namespace WebApp.ViewModels
{
    public class TravelListViewModel
    {
        public TravelListViewModel(IQueryable<TripDetails> List)
        {
            this.List = List;
        }

        public TravelListViewModel(string StartCity,string DestCity,float Cost,DateTime MinDate,DateTime MaxDate,IQueryable<TripDetails> List)
        {
            this.MinDate = MinDate;
            this.MaxDate = MaxDate;
            this.StartCity = StartCity;
            this.DestCity = DestCity;
            this.Cost = Cost;
            this.List = List;
        }
        public string DestCity { get; set; }
        public string StartCity { get; set; }
        public float Cost { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public IQueryable<TripDetails> List { get; set; }
    }
}
