﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.EmailConfirmation;
using WebApp.Services;

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

            var url =Url.Action("ConfirmAccount","Login",new { },Request.Scheme);
            
            return Content("Account created succesfully check email","text/html");
        }
        [Route("[controller]/ConfirmAccount")]
        public async Task<IActionResult> ConfirmAccountAsync([FromQuery] string userId, [FromQuery] string token)
        {

            return RedirectToRoute("Home");
        }
    }
}
