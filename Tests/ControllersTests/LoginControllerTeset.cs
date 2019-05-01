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
using WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

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

        //[Fact]
        //public async void CreateActionLinkToPasswordResetControllerAfterCreatingAccount()
        //{
        //    var accountMock = new Mock<IAccountManager>();

        //    var senderMock = new Mock<IEmailConfirmationSender>();
        //    var factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
        //    factoryMock.Setup(fm => fm.CreateCofirmationSender())
        //        .Returns(senderMock.Object);

        //    var requestMock = new Mock<HttpRequest>();
        //    var httpContext = new Mock<HttpContext>();
        //    httpContext.Setup(hm => hm.Request).Returns(requestMock.Object);
        //    var urlMock = new Mock<IUrlHelper>();
        //    urlMock.Setup(um => um.Action(
        //        "ConfirmAccount",
        //        "Login",
        //        It.IsAny<object>(),
        //        It.IsAny<string>()))
        //        .Returns("actionlink")
        //        .Verifiable();
            
        //    var controller = new LoginController(accountMock.Object, factoryMock.Object);
        //    controller.ControllerContext.HttpContext = httpContext.Object;
        //    controller.Url = urlMock.Object;

        //    await controller.RegisterAsync("username", "password", "email");

        //    urlMock.Verify();
        //}

        //[Fact]
        //public async void SendEmailAfterSuccededRegirestration()
        //{
        //    var accountMock = new Mock<IAccountManager>();

        //    var senderMock = new Mock<IEmailConfirmationSender>();
        //    var factoryMock = new Mock<IAccountEmailConfirmatorFactory>();
        //    factoryMock.Setup(fm => fm.CreateCofirmationSender())
        //        .Returns(senderMock.Object);

        //    var requestMock = new Mock<HttpRequest>();
        //    var httpContext = new Mock<HttpContext>();
        //    httpContext.Setup(hm => hm.Request).Returns(requestMock.Object);
        //    var urlMock = new Mock<IUrlHelper>();
        //    urlMock.Setup(um => um.Action(
        //        It.IsAny<string>(),
        //        It.IsAny<string>(),
        //        It.IsAny<object>(),
        //        It.IsAny<string>()))
        //        .Returns("actionlink");

        //    var controller = new LoginController(accountMock.Object, factoryMock.Object);
        //    controller.ControllerContext.HttpContext = httpContext.Object;
        //    controller.Url = urlMock.Object;

        //    await controller.RegisterAsync("username", "password", "email");

        //    senderMock.Verify(sm => sm.SendConfirmationEmailAsync(It.IsAny<string>(),"actionlink"), Times.Once);
        //}
    }
}
