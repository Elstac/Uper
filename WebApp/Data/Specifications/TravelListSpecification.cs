using LinqKit;
using System;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class TravelListSpecification : BaseSpecification<TripDetails>
    {
        public TravelListSpecification(
            string StartCity,
            string DestCity,
            DateTime? MinDate, 
            DateTime? MaxDate,
            float? Cost, 
            bool Smoking,
            int? Seats)
            : base(
                  GetCriteria(
                StartCity,
                DestCity,
                MinDate,
                MaxDate, 
                Cost, Smoking,
                Seats))
        {
            AddInclude<TripUser>(td => td.Passangers)
                .AddThenInclude(tu => tu.User);
        }

        private static Expression<Func<TripDetails,bool>> GetCriteria(
            string StartCity,
            string DestCity,
            DateTime? MinDate,
            DateTime? MaxDate,
            float? Cost,
            bool Smoking,
            int? Seats)
        {
            var pred = PredicateBuilder.New<TripDetails>(c => true);
            if (!String.IsNullOrWhiteSpace(StartCity))
            {
                pred = pred.And(c => c.StartingAddress.City.ToUpper() == StartCity.ToUpper());
            }
            if (!String.IsNullOrWhiteSpace(DestCity))
            {
                pred = pred.And(c => c.DestinationAddress.City.ToUpper() == DestCity.ToUpper());
            }
            if (MinDate != null)
            {
                pred = pred.And(c => c.Date >= MinDate);
            }
            if (MaxDate != null)
            {
                pred = pred.And(c => c.Date <= MaxDate);
            }
            if (Cost != null)
            {
                pred = pred.And(c => c.Cost <= Cost);
            }
            if (Seats != null)
            {
                pred = pred.And(c => (c.Size - c.Passangers.Count()) >= Seats);
            }
            if (Smoking)
            {
                pred = pred.And(c => c.IsSmokingAllowed == Smoking);
            }
            return pred;
        }
    }
}
