using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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

        public async Task<IActionResult> SignInAsync(string username, string password, string returnUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            var result = await signInManager.PasswordSignInAsync(username, password, true, false);

            if (!result.Succeeded)
                return Content("Sign in failed", "text/html");

            if (string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> RegisterAsync(string username, string password,string email)
        {
            var result = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = username,
                Email = email
            },password);

            if(!result.Succeeded)
                return Content("Register failed", "text/html");

            return Content("Account created", "text/html");
        }
    }
}
