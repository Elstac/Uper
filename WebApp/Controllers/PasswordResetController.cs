using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;
using WebApp.Data;
using WebApp.Models.EmailConfirmation;
using System.Collections.Generic;

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

        
        public async Task<IActionResult> SendPasswordResetAsync(string email)
        {
            var userList = userRepository.GetList(new UserByEmail(email)) as List<ApplicationUser>;

            if (userList.Count == 0)
                return BadRequest("Invalid email");

            var user = userList[0];
            var url = Url.Action("Reset", "PasswordReset", new { }, Request.Scheme);

            await passwordResetFactory.CreatePasswordResetSender(user.UserName).SendConfirmationEmailAsync(user.Id,url);

            return Content("Password reset confirmation has been sent", "html/text");
        }

        public async Task<IActionResult> ChangePasswordAsync(string id, string token, string newPassword)
        {
            await passwordResetFactory.CreatePasswordResetConfirmator().ConfirmEmailAsync(id, token, newPassword);

            return Content("Password changed successfully");
        }
    }
}
