
using Newtonsoft.Json;
using System;
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
            TemplateList list;
            using (var sr = new StreamReader(configFile))
            {
                list = (TemplateList)new JsonSerializer().Deserialize(sr, typeof(TemplateList));
            }

            if (list == null)
                throw new InvalidOperationException($"{configFile} does not contain any valid message template");

            var val = list.templates.Find((tmp) =>
            {
                return tmp.Name == messageType;
            });

            return val.Template;
        }
    }

    public class TemplateList
    {
        public List<MessageTemplate> templates { get; set; }
    }
}