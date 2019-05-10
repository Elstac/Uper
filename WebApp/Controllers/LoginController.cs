using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.EmailConfirmation;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private IAccountManager accountManager;
        private IAccountEmailConfirmatorFactory accountConfirmatorFactory;

        public LoginController(IAccountManager accountManager,IAccountEmailConfirmatorFactory accountConfirmatorFactory)
        {
            this.accountManager = accountManager;
            this.accountConfirmatorFactory = accountConfirmatorFactory;
        }
                
        public IActionResult SignIn(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        public IActionResult Register(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        public IActionResult SignOut(string returnUrl)
        {
            accountManager.SignOutAsync();

            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToRoute("Home");

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> SignInAsync(string username, string password, string returnUrl)
        {
            await accountManager.SignOutAsync();

            try
            {
                await accountManager.SignInAsync(username, password);
            }
            catch (System.Exception e)
            {
                return Content(e.Message, "text/html");
            }

            if(string.IsNullOrEmpty(returnUrl))
                return RedirectToRoute("Home");

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> RegisterAsync(string username, string password,string email,string returnurl)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };

            try
            {
                await accountManager.CreateAccountAsync(user,password);
            }
            catch(System.Exception e)
            {
                return Content(e.Message, "text/html");
            }

            await accountManager.SignInAsync(username, password);
            var url = Url.Action("ConfirmAccount", "Login", new { }, Request.Scheme);

            await accountConfirmatorFactory.CreateCofirmationSender()
                .SendConfirmationEmailAsync(user.Id, url, user.UserName);

            return Redirect(returnurl);
        }
        [Route("[controller]/ConfirmAccount")]
        public async Task<IActionResult> ConfirmAccountAsync([FromQuery] string id, [FromQuery] string token)
        {
            await accountConfirmatorFactory.CreateConfirmator()
                .ConfirmEmailAsync(id, token);

            return RedirectToRoute("Home");
        }
    }
}
