using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    public class ChatEntry:BaseEntity
    {
        public int TripId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
