using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.EmailConfirmation;
using Xunit;

namespace Tests.ControllersTests
{
    public class LoginControllerTeset
    {
        [Fact]
        public async void SignInWithAccountManagerWhenSigningIn()
        {
            var accountMock = new Mock<IAccountManager>();
            var factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
            var urlMock = new Mock<IUrlHelper>();

            var controller = new LoginController(accountMock.Object, factoryMock.Object);
            controller.Url = urlMock.Object;

            await controller.SignInAsync("username","password","ret");

            accountMock.Verify(am => am.SignInAsync("username", "password"), Times.Once);
        }

        [Fact]
        public async void SignOutUsingAccountManagerBeforeSigningIn()
        {
            var accountMock = new Mock<IAccountManager>();
            var factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
            var urlMock = new Mock<IUrlHelper>();

            var controller = new LoginController(accountMock.Object, factoryMock.Object);
            controller.Url = urlMock.Object;

            await controller.SignInAsync("username", "password", "ret");

            accountMock.Verify(hm => hm.SignOutAsync(),Times.Once);
        }

        [Fact]
        public async void UseAccountManagerRegisterUserCreatedInController()
        {
            ApplicationUser createdUser = null;
            var accountMock = new Mock<IAccountManager>();
            accountMock.Setup(am => am.CreateAccountAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()))
                .Callback<ApplicationUser,string>((user,s )=> createdUser = user);

            var factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
            var urlMock = new Mock<IUrlHelper>();

            var controller = new LoginController(accountMock.Object, factoryMock.Object);
            controller.Url = urlMock.Object;

            await controller.RegisterAsync("username", "password", "email");

            accountMock.Verify(am => am.CreateAccountAsync(It.IsAny<ApplicationUser>(),"password"), Times.Once);
            Assert.Equal("username", createdUser.UserName);
            Assert.Equal("email", createdUser.Email);
        }

        [Fact]
        public async void RedirectToReturnUrlAfterSuccesfulSignin()
        {
            var accountMock = new Mock<IAccountManager>();
            var factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
            var urlMock = new Mock<IUrlHelper>();

            var controller = new LoginController(accountMock.Object, factoryMock.Object);

            var @out = await controller.SignInAsync("username", "password", "ret") as RedirectResult;

            Assert.Equal("ret", @out.Url);
        }

        [Theory]
        [InlineData(null)]
        public async void RedirectToHomeRouteAfterSuccesfulSigninWithIncorrectReturnUrl(string returnUrl)
        {
            var accountMock = new Mock<IAccountManager>();
            var factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
            var urlMock = new Mock<IUrlHelper>();

            var controller = new LoginController(accountMock.Object, factoryMock.Object);

            var @out = await controller.SignInAsync("username", "password", returnUrl) as RedirectToRouteResult;

            Assert.Equal("Home", @out.RouteName);
        }

        [Fact]
        public void RedirectToReturnUrlAfterSuccesfulSignout()
        {
            var accountMock = new Mock<IAccountManager>();
            var factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
            var urlMock = new Mock<IUrlHelper>();

            var controller = new LoginController(accountMock.Object, factoryMock.Object);

            var @out = controller.SignOut("ret") as RedirectResult;

            Assert.Equal("ret", @out.Url);
        }

        [Theory]
        [InlineData(null)]
        public void RedirectToHomeRouteAfterSuccesfulSignOutWithIncorrectReturnUrl(string returnUrl)
        {
            var accountMock = new Mock<IAccountManager>();
            var factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
            var urlMock = new Mock<IUrlHelper>();

            var controller = new LoginController(accountMock.Object, factoryMock.Object);

            var @out = controller.SignOut(returnUrl) as RedirectToRouteResult;

            Assert.Equal("Home", @out.RouteName);
        }
    }
}
