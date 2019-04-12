using System.Collections.Generic;
using WebApp.Exceptions;

namespace WebApp.Services
{
    public interface IContentBuilder
    {
        string Template { get; set; }
        string Head { get; set; }
        List<string> BodyParts { get; set; }
        string Footer { get; set; }
        
        string BuildContent();
    }

    public class ContentBuilder : IContentBuilder
    {
        public string Template { get; set; }
        public string Head { get; set; }
        public List<string> BodyParts { get; set; }
        public string Footer { get; set; }

        public string BuildContent()
        {
            var tmp = Template;
            if (!Template.Contains("Head"))
            {
                if (!string.IsNullOrEmpty(Head))
                    throw new MessageException("Template doesnt contains placeholder for head");
            }
            else
                tmp = tmp.Replace("Head", Head);

            if (!Template.Contains("Footer"))
            {
                if (!string.IsNullOrEmpty(Head))
                    throw new MessageException("Template doesnt contains placeholder for footer");
            }
            else
                tmp = tmp.Replace("Footer", Footer);

            for (int i = 0; i < BodyParts.Count; i++)
            {
                if (!Template.Contains($"Body{i+1}"))
                    throw new MessageException($"Template doesnt contains placeholder for part {i+1} of body");

                tmp = tmp.Replace($"Body{i+1}", BodyParts[i]);
            }

            return tmp;
        } 
        
    }
}
