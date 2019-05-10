using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.EmailConfirmation;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class EmailConfirmController : Controller
    {
        private IAccountManager accountManager;
        private IAccountEmailConfirmatorFactory accountConfirmatorFactory;

        public EmailConfirmController(
            IAccountManager accountManager, 
            IAccountEmailConfirmatorFactory accountConfirmatorFactory)
        {
            this.accountManager = accountManager;
            this.accountConfirmatorFactory = accountConfirmatorFactory;
        }

        [Authorize]
        [Route("[controller]/SendConfirmationEmail")]
        public async Task<IActionResult> SendConfirmationEmailAsync(string returnUrl)
        {
            var user = await accountManager.GetUserAsync(HttpContext.User);

            var url = Url.Action("ConfirmAccount", "Login", new { }, Request.Scheme);

            await accountConfirmatorFactory
                .CreateCofirmationSender()
                .SendConfirmationEmailAsync(user.Id, url, user.UserName);

            if(string.IsNullOrEmpty(returnUrl))
                return RedirectToRoute("Home");

            return Redirect(returnUrl);
        }
    }
}
