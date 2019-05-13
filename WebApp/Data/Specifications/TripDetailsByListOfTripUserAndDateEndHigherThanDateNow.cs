﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class TripDetailsByListOfTripUserAndDateEndHigherThanDateNow : BaseSpecification<TripDetails>
    {
        public TripDetailsByListOfTripUserAndDateEndHigherThanDateNow(List<TripUser> tripUsers) : base
          (td => tripUsers.Where(tu => tu.TripId == td.Id).Count() != 0 && td.DateEnd.CompareTo(DateTime.Now) > 0)
        { }
    }
}