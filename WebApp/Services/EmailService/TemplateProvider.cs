
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace WebApp.Services
{
    public interface ITemplateProvider
    {
        string GetTemplate(string messageType);
    }

    public class JsonTemplateProvider : ITemplateProvider
    {
        private string configFile;

        public JsonTemplateProvider(string configFile)
        {
            this.configFile = configFile;
        }

        public string GetTemplate(string messageType)
        {
            var list = (TemplateList)new JsonSerializer().Deserialize(new StreamReader(configFile),typeof(TemplateList));

            var val = list.templates.Find((tmp) =>
            {
                return tmp.Name == messageType;
            });

            return val.Template;
        }
    }

    class TemplateList
    {
        public List<MessageTemplate> templates { get; set; }
    }
}