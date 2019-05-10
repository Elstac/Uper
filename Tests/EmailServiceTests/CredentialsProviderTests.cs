using Newtonsoft.Json;
using System.IO;
using WebApp.Services;
using Xunit;

namespace Tests.EmailServiceTests
{
    public class CredentialsProviderTests
    {
        private CredentialsProvider credentialsProvider;
        private string filePath = "test.json";
        public CredentialsProviderTests()
        {
            
            using (var sw = new StreamWriter(filePath))
            {
                new JsonSerializer().Serialize(sw, new SmtpClientCredentials
                {
                    Password = "password",
                    Username = "username"
                });
            }

            credentialsProvider = new CredentialsProvider(filePath);
        }

        [Fact]
        public void ReturnCredentialsFromJsonFile()
        {
            var @out= credentialsProvider.GetCredentials();

            Assert.Equal("username", @out.Username);
            Assert.Equal("password", @out.Password);
        }
    }
}
