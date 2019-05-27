using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class CollidingJoinedUserTrips : BaseSpecification<TripDetails>
    {
        public CollidingJoinedUserTrips(string userId, DateTime dateStart, DateTime dateEnd) : base
          (
            td => td.Passangers.Contains(new TripUser { UserId = userId }, new TripUserUserIdComparer()) &&
            (
            (td.Date.CompareTo(dateStart) >= 0 && td.Date.CompareTo(dateEnd) <= 0) ||
             (td.DateEnd.CompareTo(dateStart) >= 0 && td.DateEnd.CompareTo(dateEnd) <= 0) ||
             (td.Date.CompareTo(dateStart) <= 0 && td.DateEnd.CompareTo(dateEnd) >= 0)
            )
          )
        {
            AddInclude<TripUser>(td => td.Passangers)
               .AddThenInclude(tu => tu.User);

            ApplyOrderByDescending(td => td.Date);
        }
    }
}
