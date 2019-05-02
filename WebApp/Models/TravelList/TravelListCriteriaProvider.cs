using System;
using System.Linq.Expressions;
using WebApp.Data;

namespace WebApp.Models.TravelList
{
    public static class TravelListCriteriaProvider
    {
        public static Expression<Func<TripDetails, bool>> GetCriteria(string StartCity, string DestCity, DateTime? MinDate, DateTime? MaxDate, float? Cost, bool Smoking)
        {
            Expression<Func<TripDetails, bool>> criteria = c=>true;
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
