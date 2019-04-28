using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;
using WebApp.Data;
using WebApp.Models.EmailConfirmation;
using System.Collections.Generic;
using System;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class PasswordResetController : Controller
    {
        private IPasswordResetFactory passwordResetFactory;
        private IApplicationUserRepository userRepository;

        public PasswordResetController(IPasswordResetFactory passwordResetFactory, IApplicationUserRepository userRepository)
        {
            this.passwordResetFactory = passwordResetFactory;
            this.userRepository = userRepository;
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
                return BadRequest("Invalid email");

            var user = userList[0];
            var url = Url.Action("ChangePassword", "PasswordReset", new { }, Request.Scheme);

            await passwordResetFactory.CreateCofirmationSender().SendConfirmationEmailAsync(user.Id,url, user.UserName);

            return Content($"Password reset confirmation email has been sent to {email}", "text/html");
        }

        public async Task<IActionResult> ChangePasswordAsync(string id, string token, string newPassword)
        {
            await passwordResetFactory.CreateConfirmator().ConfirmEmailAsync(id, token, newPassword);

            return Content("Password changed successfully");
        }
    }
}
