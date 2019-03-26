using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public class PieciaDetailsRepository : IRepository<TripDetails>
    {
        List<TripDetails> dbMock;

        public PieciaDetailsRepository()
        {
            dbMock = new List<TripDetails>()
            {
                new TripDetails{}
            };
        }

        public void Add(TripDetails toAdd)
        {
        }

        public TripDetails GetById(int id)
        {
            if (id == 1)
                return new TripDetails { Cost = 10, Id = id, DriverId = 10 };
            else
                throw new Exception("nic");
        }

        public IEnumerable<TripDetails> GetList()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
