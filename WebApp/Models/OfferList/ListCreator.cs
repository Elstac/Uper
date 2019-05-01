using System;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;

namespace WebApp.Models.OfferList
{
    public interface IListCreator
    {
        IQueryable<TripDetails> GetList(float MaxCost, string StartCity, string DestCity,DateTime Mindate,DateTime MaxDate);
    }

    public class ListCreator : IListCreator
    {
        ITripDetailsRepository repository;

        public ListCreator(ITripDetailsRepository repository)
        {
            this.repository = repository;
        }
        public IQueryable<TripDetails> GetList(float MaxCost,string StartCity,string DestCity,DateTime MinDate,DateTime MaxDate)
        {
            IQueryable<TripDetails> TList = repository.GetAll().AsQueryable();
            Expression<Func<TripDetails, bool>> expression = c => c.Cost <= MaxCost && c.DestinationAddress.City.ToUpper() == DestCity.ToUpper() && c.StartingAddress.City.ToUpper() == StartCity.ToUpper() && c.Date >= MinDate && c.Date <= MaxDate;
            ISpecification< TripDetails > spec = new TravelListSpecification(expression);
            TList = SpecificationEvaluator<TripDetails>.EvaluateSpecification(TList, spec).AsQueryable();
            return TList;
        }
            
    }
}
