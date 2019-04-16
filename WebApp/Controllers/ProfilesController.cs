using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class ProfilesController : Controller
    {
        private IAccountManager accountManager;
        private IApplicationUserViewModelGenerator generator;
        private IApplicationUserRepository repository;

        public ProfilesController(IApplicationUserViewModelGenerator generator, IAccountManager accountManager,IApplicationUserRepository repository)
        {
            this.generator = generator;
            this.accountManager = accountManager;
            this.repository = repository;
        }

        public IActionResult MyProfile()
        {
            if (!accountManager.IsSignedIn(User))
            {
                return RedirectToAction("UnloggedException");
            }
            else
            {
                var vm = generator.ConvertAppUserToViewModel(repository.GetById(accountManager.GetUserId(User)));

                return View(vm);
            }          
        }

        public IActionResult UnloggedException()
        {
            return View();
        }
    }
}