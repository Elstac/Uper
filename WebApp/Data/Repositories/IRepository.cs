using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebApp.Data
{
    public interface IRepository<EntityType,IdType> 
    {
        IEnumerable<EntityType> GetAll();
        void Add(EntityType toAdd);
        void Remove(EntityType toRemove);
        EntityType GetById(IdType id);
        IEnumerable<EntityType> GetList(ITravelListSpecification<EntityType> specification);
    }
}
