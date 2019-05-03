using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Data.Specifications
{
    public class TravelListSpecification : BaseSpecification<TripDetails>
    {
        public TravelListSpecification(string StartCity, string DestCity, DateTime? MinDate, DateTime? MaxDate, float? Cost, bool Smoking)
            : base(GetCriteria(StartCity, DestCity, MinDate, MaxDate, Cost, Smoking))
        { }

        private static Expression<Func<TripDetails,bool>> GetCriteria(string StartCity,string DestCity,DateTime? MinDate,DateTime? MaxDate,float? Cost,bool Smoking)
        {

            Expression<Func<TripDetails, bool>> criteria = c => true;
            if (!String.IsNullOrWhiteSpace(StartCity))
            {
                var com = criteria.Compile();
                criteria = c => com(c) && c.StartingAddress.City.ToUpper() == StartCity.ToUpper();
            }
            if (!String.IsNullOrWhiteSpace(DestCity))
            {
                var com = criteria.Compile();
                criteria = c => com(c) && c.DestinationAddress.City.ToUpper() == DestCity.ToUpper();
            }
            if (MinDate != null)
            {
                var com = criteria.Compile();
                criteria = c => com(c) && c.Date >= MinDate;
            }
            if (MaxDate != null)
            {
                var com = criteria.Compile();
                criteria = c => com(c) && c.Date <= MaxDate;
            }
            if (Cost != null)
            {
                var com = criteria.Compile();
                criteria = c => com(c) && c.Cost <= Cost;
            }
            if (Smoking)
            {
                var com = criteria.Compile();
                criteria = c => com(c) && c.IsSmokingAllowed == Smoking;
            }
            return criteria;
        }
    }
}
