using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Entities;
using WebApp.Data.Repositories;
using WebApp.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class TripDetailsController : Controller
    {
        private ITripDetailsViewModelGenerator generator;
        private IAccountManager accountManager;
        private ITripUserRepository tripUserRepository;
        private ITripDetailsRepository tripDetailsRepository;

        public TripDetailsController(ITripDetailsViewModelGenerator generator, IAccountManager accountManager, ITripUserRepository tripUserRepository, ITripDetailsRepository tripDetailsRepository)
        {
            this.generator = generator;
            this.accountManager = accountManager;
            this.tripUserRepository = tripUserRepository;
            this.tripDetailsRepository = tripDetailsRepository;
        }

        /// <summary>
        /// Get details page about trip with <paramref name="id"/>. Content depends on <paramref name="viewerType"/> of viewer.
        /// </summary>
        /// <param name="id">Trip ID</param>
        /// <param name="viewerType">Type of viewer</param>
        /// <returns>Details page</returns>
        [Authorize]
        public IActionResult Index([FromQuery]int id, [FromQuery]ViewerType viewerType)
        {

           // viewerType = (ViewerType)1;
            var vm = generator.GetViewModel(id,viewerType);

            ViewData["type"] = viewerType;
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Join(int id)
        {
            TripUser tripUser = new TripUser {
                TripId = id,
                UserId = accountManager.GetUserId(HttpContext.User)
            };

            tripUserRepository.Add(tripUser);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            tripUserRepository.RemoveTripUsers(id);
            tripDetailsRepository.Remove(tripDetailsRepository.GetById(id));
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Leave(int id)
        {
            tripUserRepository.RemoveUserFromTrip(id,accountManager.GetUserId(HttpContext.User));
            return RedirectToAction("Index", "Home");
        }
    }
}
