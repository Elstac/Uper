using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class ChatEntryByTripId : BaseSpecification<ChatEntry>
    {
        public ChatEntryByTripId(int tid) : base(che => che.TripId == tid)
        {
        }
    }
}


