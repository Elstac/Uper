using System.Collections.Generic;

namespace WebApp.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        void Add(T toAdd);
        void Remove(T toRemove);
        T GetById(int id);
        IEnumerable<T> GetList(ISpecification<T> sepcification);
    }
}
