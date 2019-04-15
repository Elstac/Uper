using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private IAccountManager accountManager;

        public LoginController(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }
                
        public IActionResult SignIn(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult SignOut(string returnUrl)
        {
            if (!accountManager.IsSignedIn(User))
            {
                Content("Co ty w ogole robisz wylogowujac sie nie bedac zalogowany lepiej przemysl swoje akcje");
            }

            accountManager.SignOutAsync();

            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                return RedirectToAction("index", "home");

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> SignInAsync(string username, string password, string returnUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            try
            {
                await accountManager.SignInAsync(username, password);
            }
            catch (System.Exception e)
            {
                return Content(e.Message, "text/html");
            }

            if(string.IsNullOrEmpty(returnUrl)||!Url.IsLocalUrl(returnUrl))
                return RedirectToAction("index","home");

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> RegisterAsync(string username, string password,string email)
        {
            try
            {
                await accountManager.CreateAccountAsync(username,password,email);
            }
            catch(System.Exception e)
            {
                return Content(e.Message, "text/html");
            }

            return Content("Account created succesfully","text/html");
        }
    }
}
