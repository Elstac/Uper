using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;
using WebApp.Models.EmailConfirmation;
using WebApp.Models.HtmlNotifications;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class PasswordResetController : Controller
    {
        private IPasswordResetFactory passwordResetFactory;
        private IApplicationUserRepository userRepository;
        private INotificationProvider notificationProvider;

        public PasswordResetController(
            IPasswordResetFactory passwordResetFactory,
            IApplicationUserRepository userRepository,
            INotificationProvider notificationProvider)
        {
            this.passwordResetFactory = passwordResetFactory;
            this.userRepository = userRepository;
            this.notificationProvider = notificationProvider;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangePassword(string id, string token)
        {
            ViewData["id"] = id;
            ViewData["token"] = token;
            return View();
        }
        
        public async Task<IActionResult> SendPasswordResetAsync(string email)
        {
            var userList = userRepository.GetList(new UserByEmail(email)) as List<ApplicationUser>;

            if (userList.Count == 0)
            {
                notificationProvider.SetNotification(
                    HttpContext.Session,
                    "res-fail",
                    "Invalid email address");

                return RedirectToAction("Index", "PasswordReset");
            }

            var user = userList[0];
            var url = Url.Action("ChangePassword", "PasswordReset", new { }, Request.Scheme);

            await passwordResetFactory.CreateCofirmationSender().SendConfirmationEmailAsync(user.Id,url, user.UserName);

            notificationProvider.SetNotification(
                HttpContext.Session,
                "res-suc",
                $"Password reset confirmation email has been sent to {email}");

            return RedirectToAction("SignIn", "Login");
        }

        public async Task<IActionResult> ChangePasswordAsync(string id, string token, string newPassword)
        {
            try
            {
                await passwordResetFactory.CreateConfirmator().ConfirmEmailAsync(id, token, newPassword);
            }
            catch (Exception)
            {
                notificationProvider.SetNotification(
                HttpContext.Session,
                "res-fail",
                "Error occurs while changing password");

                return RedirectToRoute("Home");
            }

            notificationProvider.SetNotification(
                HttpContext.Session,
                "res-suc",
                "Password changed successfully");

            return RedirectToAction("SignIn", "Login");
        }
    }
}
