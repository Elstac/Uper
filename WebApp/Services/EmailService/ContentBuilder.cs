using System.Collections.Generic;
using WebApp.Exceptions;
using System.Text.RegularExpressions;

namespace WebApp.Services
{
    public interface IContentBuilder
    {
        string BuildContent(string template, IMessageBodyDictionary body);
    }

    public class ContentBuilder : IContentBuilder
    {
        private Regex regex;

        public ContentBuilder(Regex regex)
        {
            this.regex = regex;
        }

        public string BuildContent(string template, IMessageBodyDictionary body)
        {
            var matches = regex.Matches(template);
            var tmp = template;
            foreach (var match in matches)
            {
                var matchS = match.ToString();
                var replacement = body.GetReplacement(matchS);

                if (replacement == "")
                    tmp = tmp.Remove(tmp.IndexOf(matchS),matchS.Length);
                else
                    tmp = tmp.Replace(matchS, body.GetReplacement(matchS));
            }

            return tmp;
        } 
        
    }
}
