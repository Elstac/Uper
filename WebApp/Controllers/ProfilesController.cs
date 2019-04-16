using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Repositories;
using WebApp.Models;

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
        [Authorize]
        public IActionResult MyProfile()
        {
            var vm = generator.ConvertAppUserToViewModel(repository.GetById(accountManager.GetUserId(User)));

            return View(vm);        
        }

        public IActionResult UnloggedException()
        {
            return View();
        }
    }
}