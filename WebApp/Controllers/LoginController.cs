using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private IAccountManager accountManager;
        private IEmailService emailService;

        public LoginController(IAccountManager accountManager,IEmailService emailService)
        {
            this.accountManager = accountManager;
            this.emailService = emailService;
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

            emailService.SendMail("Uper", email, "Standard", new MessageBody
            {
                Head = $"Hi {username}",
                BodyParts = new System.Collections.Generic.List<string>
                {
                    $"Your account name: {username}"
                },
                Footer = "Bye"
            });

            return Content("Account created succesfully check email","text/html");
        }
    }
}
