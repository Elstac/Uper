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
        
        public IQueryable<TripDetails> List { get; set; }
    }
}
