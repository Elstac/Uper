using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebApp.Data.Specifications;

namespace WebApp.Data
{
    public interface IRepository<EntityType,IdType> 
    {
        IEnumerable<EntityType> GetAll();
        void Add(EntityType toAdd);
        void Remove(EntityType toRemove);
        EntityType GetById(IdType id);
        IEnumerable<EntityType> GetList(ISpecification<EntityType> specification);
        void Update(EntityType entity);
    }
}