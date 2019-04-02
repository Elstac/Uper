using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebApp.Data
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetAll();
        void Add(ApplicationUser toAdd);
        void Remove(ApplicationUser toRemove);
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetList(ISpecification<ApplicationUser> specification);
    }
}
