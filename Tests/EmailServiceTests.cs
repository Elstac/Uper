using WebApp.Services;
using Moq;
using Xunit;
using MimeKit;

namespace Tests
{
    public class EmailServiceTests
    {
        private EmailService emailService;
        private Mock<ISmtpClientProvider> smtpMock;
        private Mock<ITemplateProvider> tempMock;
        private Mock<IContentBuilder> cbMock;
        private Mock<IMessageBuilder> mbMock;
        private Mock<ICredentialsProvider> credentialsMock;

        private MimeMessage message;

        public EmailServiceTests()
        {
            message = new MimeMessage();

            smtpMock = new Mock<ISmtpClientProvider>();
            tempMock = new Mock<ITemplateProvider>();

            cbMock = new Mock<IContentBuilder>();
           
            mbMock = new Mock<IMessageBuilder>();
            mbMock.Setup(m => m.BuildMessage()).Returns(message);

            credentialsMock = new Mock<ICredentialsProvider>();
            credentialsMock.Setup(m => m.GetCredentials()).Returns(new SmtpClientCredentials
                                                                   {
                                                                       Password = "password",
                                                                       Username = "username"
                                                                   });

            emailService = new EmailService(smtpMock.Object,
                tempMock.Object,
                cbMock.Object,
                mbMock.Object,
                credentialsMock.Object);
        }
        
        [Fact]
        public void SendCorrectMessage()
        {
            emailService.SendMail("","","",null);

            smtpMock.Verify(m => m.SendMessage(message), Times.Once);
        }

        [Fact]
        public void CallMessageBuilderBuildBeforeSending()
        {
            emailService.SendMail("", "", "", null);

            mbMock.Verify(m => m.BuildMessage(), Times.Once);
        }

        [Fact]
        public void GetCredentialsBeforeConnectig()
        {
            emailService.SendMail("", "", "", null);

            credentialsMock.Verify(m => m.GetCredentials(), Times.Once);
        }

        [Fact]
        public void PassCorrectCredentialsToSmtpProvider()
        {
            emailService.SendMail("", "", "", null);

            smtpMock.Verify(m => m.Connect("username","password"), Times.Once);
        }


    }
}
