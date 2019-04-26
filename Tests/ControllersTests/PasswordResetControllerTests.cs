using Moq;
using WebApp.Controllers;
using WebApp.Data.Repositories;
using WebApp.Models.EmailConfirmation;
using Xunit;

namespace Tests.ControllersTests
{
    public class PasswordResetControllerTests
    {
        private PasswordResetController controller;
        private Mock<IApplicationUserRepository> repoMock;
        private Mock<IPasswordResetFactory> factoryMock;
        private Mock<IEmailConfirmationSender> senderMock;
        private Mock<IEmailConfirmator> confirmatorMock;

        public PasswordResetControllerTests()
        {
            repoMock = new Mock<IApplicationUserRepository>();

            factoryMock = new Mock<IPasswordResetFactory>();

            senderMock = new Mock<IEmailConfirmationSender>();

            confirmatorMock = new Mock<IEmailConfirmator>();

            controller = new PasswordResetController(factoryMock.Object, repoMock.Object);
        }

    }
}
