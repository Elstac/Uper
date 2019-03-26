using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetList();
        void Add(T toAdd);
        void Remove(int id);
        T GetById(int id);
    }
}
