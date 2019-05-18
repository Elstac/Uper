using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class NotFinishedJoinedUsersTrips : BaseSpecification<TripDetails>
    {
        public NotFinishedJoinedUsersTrips(string userId) : base
          (
            td => td.Passangers.Contains(new TripUser { UserId = userId }, new TripUserUserIdComparer()) &&
            td.DateEnd.CompareTo(DateTime.Now) > 0
          )
        {
            AddInclude<TripUser>(td => td.Passangers)
                .AddThenInclude(tu => tu.User);

            ApplyOrderByDescending(td => td.Date);
        }
    }

    public class TripUserUserIdComparer : IEqualityComparer<TripUser>
    {
        public bool Equals(TripUser x, TripUser y)
        {
            return x.UserId == y.UserId;
        }

        public int GetHashCode(TripUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
