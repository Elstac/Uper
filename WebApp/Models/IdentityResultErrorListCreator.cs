using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebApp.Models
{
    public interface IIdentityResultErrorListCreator
    {
        IEnumerable<string> CreateErrorList(IdentityResult result);
    }

    public class IdentityResultErrorListCreator : IIdentityResultErrorListCreator
    {
        public IEnumerable<string> CreateErrorList(IdentityResult result)
        {
            throw new System.NotImplementedException();
        }
    }
}
