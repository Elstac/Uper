using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Models
{
    public interface IIdentityResultErrorHtmlCreator
    {
        string CreateErrorHtml(IdentityResult result);
    }

    public class IdentityResultErrorHtmlCreator : IIdentityResultErrorHtmlCreator
    {
        public string CreateErrorHtml(IdentityResult result)
        {
            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.Append($"<p>{error.Description}</p>");
            }

            return sb.ToString();
        }
    }
}
