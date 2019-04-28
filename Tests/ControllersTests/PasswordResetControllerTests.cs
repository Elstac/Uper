using Moq;
using WebApp.Controllers;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;
using WebApp.Models.EmailConfirmation;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

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
        }

        private void SetupEmailSenderTest()
        {
            repoMock = new Mock<IApplicationUserRepository>();
            repoMock.Setup(rm => rm.GetList(It.IsAny<ISpecification<ApplicationUser>>())).Returns(
                new List<ApplicationUser>
                {
                    new ApplicationUser{Id = "userId", UserName = "userName"}
                });

            senderMock = new Mock<IEmailConfirmationSender>();
            factoryMock = new Mock<IPasswordResetFactory>();
            factoryMock.Setup(fm => fm.CreateCofirmationSender()).Returns(senderMock.Object);

            var urlMock = new Mock<IUrlHelper>();
            var requestMock = new Mock<HttpRequest>();
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(hm => hm.Request).Returns(requestMock.Object);

            controller = new PasswordResetController(factoryMock.Object, repoMock.Object);
            controller.Url = urlMock.Object;
            controller.ControllerContext.HttpContext = httpContext.Object;
        }

        [Fact]
        public async void CreatePasswordResetSenderBeforeSendingEmail()
        {
            SetupEmailSenderTest();

            await controller.SendPasswordResetAsync("email@email.com");

            factoryMock.Verify(fm => fm.CreateCofirmationSender(), Times.Once());
        }

        [Fact]
        public async void SendEmailBasedOnUserIdAndUserNameWithWhateverUrlUsingCreatedSender()
        {
            SetupEmailSenderTest();

            await controller.SendPasswordResetAsync("email@email.com");

            senderMock.Verify(sm => sm.SendConfirmationEmailAsync("userId", It.IsAny<string>(), "userName"), Times.Once());
        }

        [Fact]
        public async void GetUserByGivenEmailBeforeSendingEmail()
        {
            SetupEmailSenderTest();

            await controller.SendPasswordResetAsync("email@email.com");

            repoMock.Verify(rm => rm.GetList(It.IsAny<ISpecification<ApplicationUser>>()), Times.Once());
        }

        [Fact]
        public async void CreatePasswordConfirmatorBeforeResetingPassword()
        {
            repoMock = new Mock<IApplicationUserRepository>();
            confirmatorMock = new Mock<IEmailConfirmator>();
            factoryMock = new Mock<IPasswordResetFactory>();
            factoryMock.Setup(fm => fm.CreateConfirmator()).Returns(confirmatorMock.Object);

            controller = new PasswordResetController(factoryMock.Object, repoMock.Object);

            await controller.ChangePasswordAsync("id","token","password");

            factoryMock.Verify(fm => fm.CreateConfirmator(),Times.Once());
        }

        [Fact]
        public async void ConfirmPasswordReset()
        {
            repoMock = new Mock<IApplicationUserRepository>();
            confirmatorMock = new Mock<IEmailConfirmator>();
            factoryMock = new Mock<IPasswordResetFactory>();
            factoryMock.Setup(fm => fm.CreateConfirmator()).Returns(confirmatorMock.Object);

            controller = new PasswordResetController(factoryMock.Object, repoMock.Object);

            await controller.ChangePasswordAsync("id", "token", "password");

            confirmatorMock.Verify(cm => cm.ConfirmEmailAsync("id","token","password"), Times.Once());
        }

        [Fact]
        public void RetutnViewWithIdAndToken()
        {
            repoMock = new Mock<IApplicationUserRepository>();
            factoryMock = new Mock<IPasswordResetFactory>();
            controller = new PasswordResetController(factoryMock.Object, repoMock.Object);

            var view = controller.ChangePassword("xd","discard");

            var viewResult = Assert.IsType<ViewResult>(view);
            Assert.Equal("xd", viewResult.ViewData["id"]);
            Assert.Equal("discard", viewResult.ViewData["token"]);
        }
    }
}