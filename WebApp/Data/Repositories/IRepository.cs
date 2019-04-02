using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebApp.Data
{
    public interface IRepository<T> 
    {
        IEnumerable<T> GetAll();
        void Add(T toAdd);
        void Remove(T toRemove);
        T GetById(int id);
        IEnumerable<T> GetList(ISpecification<T> specification);
    }
}
