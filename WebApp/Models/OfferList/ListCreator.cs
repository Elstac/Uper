using System;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Data;
using WebApp.Data.Specifications;

namespace WebApp.Models.OfferList
{
    interface IListCreator
    {

    }

    public class ListCreator
    {
        IRepository<TripDetails, int> repository;

        public ListCreator(IRepository<TripDetails,int> repository)
        {
            this.repository = repository;
        }
        public IQueryable<TripDetails> GetList(float MaxCost,string StartCity,string DestCity)
        {
            IQueryable<TripDetails> TList = repository.GetAll().AsQueryable();
            Expression<Func<TripDetails, bool>> expression = c => c.Cost <= MaxCost && c.DestinationAddress.City.ToUpper() == DestCity.ToUpper() && c.StartingAddress.City.ToUpper() == StartCity.ToUpper();
            ISpecification< TripDetails > spec = new BaseSpecification(expression);
            TList = SpecificationEvaluator<TripDetails>.EvaluateSpecification(TList, spec).AsQueryable();
            return TList;
        }
            
    }

    internal class BaseSpecification : BaseSpecification<TripDetails>
    {
        public BaseSpecification(Expression<Func<TripDetails, bool>> criteria) : base(criteria)
        {
        }
    }
}
