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
            tempMock.Setup(m => m.GetTemplate(It.IsAny<string>())).Returns("template");

            cbMock = new Mock<IContentBuilder>();
           

            credentialsMock = new Mock<ICredentialsProvider>();
            credentialsMock.Setup(m => m.GetCredentials()).Returns(new SmtpClientCredentials
                                                                   {
                                                                       Password = "password",
                                                                       Username = "username"
                                                                   });

            emailService = new EmailService(smtpMock.Object,
                tempMock.Object,
                cbMock.Object,
                credentialsMock.Object);
        }
        


        [Fact]
        public void CallContentBuilderBeforeSending()
        {
            emailService.SendMail("", "", "", new MessageBody());

            cbMock.Verify(m => m.BuildContent(), Times.Once);
        }

        [Fact]
        public void GetCredentialsBeforeConnectig()
        {
            emailService.SendMail("", "", "", new MessageBody());

            credentialsMock.Verify(m => m.GetCredentials(), Times.Once);
        }

        [Fact]
        public void PassCorrectCredentialsToSmtpProvider()
        {
            emailService.SendMail("", "", "", new MessageBody());

            smtpMock.Verify(m => m.Connect("username","password"), Times.Once);
        }

        [Fact]
        public void GetCorrectTypeTemplate()
        {
            emailService.SendMail("", "", "type", new MessageBody());

            tempMock.Verify(m => m.GetTemplate("type"), Times.Once);
        }
    }
}
