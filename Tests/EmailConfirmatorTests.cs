using Moq;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Exceptions;
using WebApp.Models.EmailConfirmation;
using Xunit;

namespace Tests
{
    public class EmailConfirmatorTests
    {
        private EmailConfirmator emailConfirmator;

        private Mock<IApplicationUserRepository> repoMock;
        private Mock<IConfirmationProvider> confirmMock;
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
            
            confirmMock = new Mock<IConfirmationProvider>();

            emailConfirmator = new EmailConfirmator(confirmMock.Object,repoMock.Object);
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

        [Fact]
        public async void ThrowArgumentNullExceptionIfUserDoesntExistsConfirm()
        {
            await Assert.ThrowsAsync<OwnNullArgumentException>(() =>
            emailConfirmator.ConfirmEmailAsync("null", "token"));
        }
    }
}
