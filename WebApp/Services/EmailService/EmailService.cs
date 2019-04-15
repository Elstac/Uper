using MimeKit;
namespace WebApp.Services
{
    /// <summary>
    /// Service responslible for generating and sending messages based on given template
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send email message
        /// </summary>
        /// <param name="from">Message sender address</param>
        /// <param name="to">Message reciver address</param>
        /// <param name="messageType">Type of message template</param>
        /// <param name="body">Dictionary of replacements in template</param>
        void SendMail(string from, string to, string messageType, IMessageBodyDictionary body);
    }

    public class EmailService : IEmailService
    {
        private ISmtpClientProvider smtpClient;
        private ITemplateProvider templateProvider;
        private IContentBuilder contentBuilder;
        private ICredentialsProvider credentialsProvider;

        public EmailService(ISmtpClientProvider smtpClient,
                            ITemplateProvider templateProvider, 
                            IContentBuilder contentBuilder, 
                            ICredentialsProvider credentialsProvider)
        {
            this.smtpClient = smtpClient;
            this.templateProvider = templateProvider;
            this.contentBuilder = contentBuilder;
            this.credentialsProvider = credentialsProvider;
        }
        public void SendMail(string from, string to, string messageType, IMessageBodyDictionary body)
        {
            var temp = templateProvider.GetTemplate(messageType);
            
            var content = contentBuilder.BuildContent(temp,body);

            //Create message
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(to));
            message.From.Add(new MailboxAddress(from));
            message.Body = new TextPart("html")
            {
                Text = content
            };

            var cred = credentialsProvider.GetCredentials();

            smtpClient.Connect(cred.Username, cred.Password);
            smtpClient.SendMessage(message);
            smtpClient.Diconnect();
        }
    }
}
