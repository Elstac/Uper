using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Models
{
    public interface IIdentityResultErrorCreator
    {
        string CreateError(IdentityResult result);
    }

    public class IdentityResultErrorHtmlCreator : IIdentityResultErrorCreator
    {
        public string CreateError(IdentityResult result)
        {
            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.Append($"<p>{error.Description}</p>");
            }

            return sb.ToString();
        }
    }

    public class IdentityResultErrorTextCreator : IIdentityResultErrorCreator
    {
        public string CreateError(IdentityResult result)
        {
            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.Append($"{error.Description}");
            }

            return sb.ToString();
        }
    }
}
