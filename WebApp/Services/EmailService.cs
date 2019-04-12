
namespace WebApp.Services
{
    public interface IEmailSservice
    {
        void SendMail(string from, string to, string messageType, MessageBody body);
    }

    public class EmailService : IEmailSservice
    {
        private ISmtpClientProvider smtpClient;
        private ITemplateProvider templateProvider;
        private IContentBuilder contentBuilder;
        private IMessageBuilder messageBuilder;
        private ICredentialsProvider credentialsProvider;

        public EmailService(ISmtpClientProvider smtpClient,
                            ITemplateProvider templateProvider, 
                            IContentBuilder contentBuilder, 
                            IMessageBuilder messageBuilder,
                            ICredentialsProvider credentialsProvider)
        {
            this.smtpClient = smtpClient;
            this.templateProvider = templateProvider;
            this.contentBuilder = contentBuilder;
            this.messageBuilder = messageBuilder;
            this.credentialsProvider = credentialsProvider;
        }

        public void SendMail(string from, string to, string messageType, MessageBody body)
        {
            throw new System.NotImplementedException();
        }
    }
}
