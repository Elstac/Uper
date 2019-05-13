using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebApp.Controllers;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.EmailConfirmation;
using WebApp.Models.HtmlNotifications;
using Xunit;

namespace Tests.ControllersTests
{
    public class LoginControllerTeset
    {
        private LoginController controller;
        private Mock<IAccountManager> accountMock;
        private Mock<IAccountEmailConfirmatorFactory> factoryMock;
        private Mock<IUrlHelper> urlMock;
        private Mock<INotificationProvider> notificationMock;

        public LoginControllerTeset()
        {
            accountMock = new Mock<IAccountManager>();
            factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
            urlMock = new Mock<IUrlHelper>();

            notificationMock = new Mock<INotificationProvider>();

            controller = new LoginController(accountMock.Object, factoryMock.Object, notificationMock.Object);
            controller.Url = urlMock.Object;
        }

        private void SignInSetup()
        {
            var sessMock = new Mock<ISession>();

            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(cm => cm.Session).Returns(sessMock.Object);

            controller.ControllerContext.HttpContext = contextMock.Object;
        }

        [Fact]
        public async void SignInWithAccountManagerWhenSigningIn()
        {
            SignInSetup();
            await controller.SignInAsync("username","password","ret");

            accountMock.Verify(am => am.SignInAsync("username", "password"), Times.Once);
        }

        [Fact]
        public async void SignOutUsingAccountManagerBeforeSigningIn()
        {
            SignInSetup();
            await controller.SignInAsync("username", "password", "ret");

            accountMock.Verify(hm => hm.SignOutAsync(),Times.Once);
        }

        [Fact]
        public async void UseAccountManagerRegisterUserCreatedInController()
        {
            ApplicationUser createdUser = null;
            accountMock = new Mock<IAccountManager>();
            accountMock.Setup(am => am.CreateAccountAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()))
                .Callback<ApplicationUser,string>((user,s )=> 
                {
                    createdUser = user;
                }).Returns(Task.CompletedTask);

            var controller = new LoginController(accountMock.Object, factoryMock.Object,notificationMock.Object);
            controller.Url = urlMock.Object;

            var context = new DefaultHttpContext();
            var sessMock = new Mock<ISession>();
            context.Session = sessMock.Object;

            controller.ControllerContext.HttpContext = context;

            await controller.RegisterAsync("username", "password", "email","ret");

            accountMock.Verify(am => am.CreateAccountAsync(It.IsAny<ApplicationUser>(),"password"), Times.Once);
            Assert.Equal("username", createdUser.UserName);
            Assert.Equal("email", createdUser.Email);
        }

        [Fact]
        public async void RedirectToReturnUrlAfterSuccesfulSignin()
        {
            SignInSetup();
            var @out = await controller.SignInAsync("username", "password", "ret") as RedirectResult;

            Assert.Equal("ret", @out.Url);
        }

        [Theory]
        [InlineData(null)]
        public async void RedirectToHomeRouteAfterSuccesfulSigninWithIncorrectReturnUrl(string returnUrl)
        {
            SignInSetup();
            var @out = await controller.SignInAsync("username", "password", returnUrl) as RedirectToRouteResult;

            Assert.Equal("Home", @out.RouteName);
        }

        [Fact]
        public void RedirectToReturnUrlAfterSuccesfulSignout()
        {
            var @out = controller.SignOut("ret") as RedirectResult;

            Assert.Equal("ret", @out.Url);
        }

        [Theory]
        [InlineData(null)]
        public void RedirectToHomeRouteAfterSuccesfulSignOutWithIncorrectReturnUrl(string returnUrl)
        {
            var @out = controller.SignOut(returnUrl) as RedirectToRouteResult;

            Assert.Equal("Home", @out.RouteName);
        }
    }
}
