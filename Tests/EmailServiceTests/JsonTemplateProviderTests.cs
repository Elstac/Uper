using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WebApp.Services;
using Xunit;

namespace Tests.EmailServiceTests
{
    public class JsonTemplateProviderTests:IDisposable
    {
        private JsonTemplateProvider jsonTemplateProvider;
        private string configFile = "conf.json";

        public JsonTemplateProviderTests()
        {
            using (var sw = new StreamWriter(configFile))
            {
                new JsonSerializer().Serialize(sw, new TemplateList
                {
                    templates = new List<MessageTemplate>()
                    {
                        new MessageTemplate
                        {
                            Name = "name1",
                            Template="temp1"
                        },
                        new MessageTemplate
                        {
                            Name = "name2",
                            Template="temp2"
                        }
                    }
                });
            }
        }

        public void Dispose()
        {
            File.Delete(configFile);
        }

        [Fact]
        public void GetTemplateFromJsonFileWithGivenName()
        {
            jsonTemplateProvider = new JsonTemplateProvider(configFile);

            var @out = jsonTemplateProvider.GetTemplate("name1");

            Assert.Equal("temp1", @out);
        }

        [Fact]
        public void ThrowsIfConfigFileIsEmpty()
        {
            File.Create("test.json").Close();

            Assert.Throws<InvalidOperationException>(() => jsonTemplateProvider.GetTemplate(configFile));

            File.Delete("test.json");
        }
    }
}
