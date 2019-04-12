using System;

namespace WebApp.Services
{
    public interface ICredentialsProvider
    {
        SmtpClientCredentials GetCredentials();
    }

    public class CredentialsProvider : ICredentialsProvider
    {
        public SmtpClientCredentials GetCredentials()
        {
            throw new NotImplementedException();
        }
    }
}
