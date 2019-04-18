using Moq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Exceptions;
using WebApp.Models;
using WebApp.Models.EmailConfirmation;
using WebApp.Services;
using Xunit;

namespace Tests
{
    public class EmailConfirmatorTests
    {
        private EmailConfirmatorAsync emailConfirmator;

        private Mock<IApplicationUserRepository> repoMock;
        private Mock<IMessageBodyDictionary> bodyMock;
        private Mock<IConfirmationProvider> confirmMock;
        private Mock<IEmailService> emailMock;
        private string result;
        private ApplicationUser user;

        public EmailConfirmatorTests()
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
            confirmMock = new Mock<IConfirmationProvider>();
            confirmMock.Setup(m => m.GenerateTokenAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult("token"));

            emailMock = new Mock<IEmailService>();

            emailConfirmator = new EmailConfirmatorAsync(confirmMock.Object, emailMock.Object, bodyMock.Object, repoMock.Object,"type");
        }

        [Fact]
        public async void ReplaceLinkWithUrlWitIdAndTokenInBodyBeforeSending()
        {
            await emailConfirmator.SendConfirmationEmailAsync("a","www.url/controller/action");

            bodyMock.Verify(m => m.AddReplacement("www.url/controller/action?id=a&token=token", "{Link}"));
        }

        [Fact]
        public async void GenerateTokenBeforeSending()
        {
            await emailConfirmator.SendConfirmationEmailAsync("a", "www.url/controller/action");

            confirmMock.Verify(m => m.GenerateTokenAsync(user), Times.Once);
        }

        [Fact]
        public async void SendMailToUserEmailAddressFromUperOfGivenType()
        {
            await emailConfirmator.SendConfirmationEmailAsync("a", "www.url/controller/action");

            emailMock.Verify(m => m.SendMail("Uper", "useremail", "type", bodyMock.Object), Times.Once);
        }

        [Fact]
        public async void ThrowArgumentNullExceptionIfUserDoesntExists()
        {
            await Assert.ThrowsAsync<OwnNullArgumentException>(() => 
            emailConfirmator.SendConfirmationEmailAsync("null", "www.url/controller/action"));
        }
        
        [Fact]
        public async void GetUserFromRepositoryByRecivedId()
        {
            await emailConfirmator.ConfirmEmailAsync("a", "token");

            repoMock.Verify(m => m.GetById("a"), Times.Once);
        }

        [Fact]
        public async void CallConfirmBechviourWithRecivedUserTokenAndParameters()
        {
            await emailConfirmator.ConfirmEmailAsync("a", "token",1,"10");

            confirmMock.Verify(m => m.ConfirmAsync(user,"token",1,"10"), Times.Once);
        }

        [Fact]
        public async void PassToConfirmBehaviorRecreatedTokenBasedOnRecivedToken()
        {
            await emailConfirmator.ConfirmEmailAsync("a", "token with spaces");

            confirmMock.Verify(m => m.ConfirmAsync(user, "token+with+spaces"), Times.Once);
        }
    }
}
