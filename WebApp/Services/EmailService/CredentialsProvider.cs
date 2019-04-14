using Newtonsoft.Json;
using System.IO;

namespace WebApp.Services
{
    public interface ICredentialsProvider
    {
        SmtpClientCredentials GetCredentials();
    }

    public class CredentialsProvider : ICredentialsProvider
    {
        private string filePath;

        public CredentialsProvider(string filePath)
        {
            this.filePath = filePath;
        }

        public SmtpClientCredentials GetCredentials()
        {
            return new JsonSerializer().Deserialize(new StreamReader(filePath), typeof(SmtpClientCredentials))as SmtpClientCredentials;
        }
    }
}
