using Moq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Exceptions;
using WebApp.Models.EmailConfirmation;
using WebApp.Services;
using Xunit;

namespace Tests
{
    public class EmailConfirmatorSenderTests
    {
        private EmailConfirmatorSender emailConfirmator;

        private Mock<IApplicationUserRepository> repoMock;
        private Mock<IMessageBodyProvider> bodyProvMock;
        private Mock<IMessageBodyDictionary> bodyMock;
        private Mock<IEmailService> emailMock;
        private Mock<IConfirmationTokenProvider> tokenMock;
        private ApplicationUser user;

        public EmailConfirmatorSenderTests()
        {
            user = new ApplicationUser
            {
                Email = "useremail"
            };

            var n = new ApplicationUser();
            n = null;
            repoMock = new Mock<IApplicationUserRepository>();
            repoMock.Setup(m => m.GetById(It.IsAny<string>()))
                .Returns(user);
            repoMock.Setup(m => m.GetById("null"))
                .Returns(n);

            bodyMock = new Mock<IMessageBodyDictionary>();
            bodyProvMock = new Mock<IMessageBodyProvider>();
            bodyProvMock.Setup(bpm => bpm.GetBody(It.IsAny<object[]>())).Returns(bodyMock.Object);

            tokenMock = new Mock<IConfirmationTokenProvider>();
            tokenMock.Setup(m => m.GenerateTokenAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult("token"));

            emailMock = new Mock<IEmailService>();

            emailConfirmator = new EmailConfirmatorSender(emailMock.Object, bodyProvMock.Object, repoMock.Object,tokenMock.Object, "type");
        }

        [Fact]
        public async void ReplaceLinkWithUrlWitIdAndTokenInBodyBeforeSending()
        {
            await emailConfirmator.SendConfirmationEmailAsync("a", "www.url/controller/action");

            bodyMock.Verify(m => m.AddReplacement("www.url/controller/action?id=a&token=token", "{Link}"));
        }

        [Fact]
        public async void GenerateTokenBeforeSending()
        {
            await emailConfirmator.SendConfirmationEmailAsync("a", "www.url/controller/action");

            tokenMock.Verify(m => m.GenerateTokenAsync(user), Times.Once);
        }

        [Fact]
        public async void SendMailToUserEmailAddressFromUperOfGivenType()
        {
            await emailConfirmator.SendConfirmationEmailAsync("a", "www.url/controller/action");

            emailMock.Verify(m => m.SendMail("Uper", "useremail", "type", bodyMock.Object), Times.Once);
        }

        [Fact]
        public async void ThrowArgumentNullExceptionIfUserDoesntExistsSendEmail()
        {
            await Assert.ThrowsAsync<OwnNullArgumentException>(() =>
            emailConfirmator.SendConfirmationEmailAsync("null", "www.url/controller/action"));
        }
    }
}
