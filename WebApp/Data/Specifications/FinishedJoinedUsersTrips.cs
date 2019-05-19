using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class FinishedJoinedUsersTrips: BaseSpecification<TripDetails>
    {
        public FinishedJoinedUsersTrips(string userId) : base
          (
            td => td.Passangers.Contains(new TripUser { UserId = userId }, new TripUserUserIdComparer()) &&
            td.DateEnd.CompareTo(DateTime.Now) < 0
          )
        {
            AddInclude<TripUser>(td => td.Passangers)
                .AddThenInclude(tu => tu.User);

            ApplyOrderByDescending(td => td.Date);
        }
    }
}
