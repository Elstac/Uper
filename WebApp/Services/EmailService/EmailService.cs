using MimeKit;
namespace WebApp.Services
{
    public interface IEmailService
    {
        void SendMail(string from, string to, string messageType, MessageBody body);
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

        public void SendMail(string from, string to, string messageType, MessageBody body)
        {
            var temp = templateProvider.GetTemplate(messageType);

            contentBuilder.Template = temp;
            contentBuilder.Head = body.Head;
            contentBuilder.Footer = body.Footer;
            contentBuilder.BodyParts = body.BodyParts;

            var content = contentBuilder.BuildContent();

            var message = new MimeKit.MimeMessage();
            message.To.Add(new MailboxAddress(to));
            message.From.Add(new MailboxAddress(from));
            message.Body = new TextPart
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
