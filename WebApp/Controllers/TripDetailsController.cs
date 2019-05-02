using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Data.Repositories;
using WebApp.Models;
using System.Linq.Expressions;
using WebApp.Data.Specifications;
using System.Collections.Generic;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class TripDetailsController : Controller
    {
        private ITripDetailsViewModelProvider generator;
        private IAccountManager accountManager;
        private ITripUserRepository tripUserRepository;
        private ITripDetailsRepository tripDetailsRepository;
        private IViewerTypeMapper viewerTypeMapper;
        private IApplicationUserRepository applicationUserRepository;

        public TripDetailsController(ITripDetailsViewModelProvider generator, IAccountManager accountManager, ITripUserRepository tripUserRepository,
            ITripDetailsRepository tripDetailsRepository,IViewerTypeMapper viewerTypeMapper, IApplicationUserRepository applicationUserRepository)
        {
            this.generator = generator;
            this.accountManager = accountManager;
            this.tripUserRepository = tripUserRepository;
            this.tripDetailsRepository = tripDetailsRepository;
            this.viewerTypeMapper = viewerTypeMapper;
            this.applicationUserRepository = applicationUserRepository;
        }

        /// <summary>
        /// Get details page about trip with <paramref name="id"/>. Content depends on <paramref name="viewerType"/> of viewer.
        /// </summary>
        /// <param name="id">Trip ID</param>
        /// <param name="viewerType">Type of viewer</param>
        /// <returns>Details page</returns>
        [Authorize]
        public IActionResult Index(int id , [FromQuery]ViewerType viewerType)
        {

            // viewerType = (ViewerType)1;
            var userid = accountManager.GetUserId(HttpContext.User);
            var user = applicationUserRepository.GetById(userid);
            var data = tripDetailsRepository.GetTripWithPassengersById(id);
            viewerType = viewerTypeMapper.GetViewerType(user, data);


            var vm = generator.GetViewModel(id, viewerType);

            //--------------------------------------------------------
            if (viewerType != (ViewerType)0)
            {
                var x = vm.PassangersUsernames.ConvertAll(p => p);
                vm.PassangersUsernames.Clear();
                foreach (string uid in x)
                {
                    vm.PassangersUsernames.Add(applicationUserRepository.GetById(uid).UserName);
                }
            }
            //---------------------------------------------------------

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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveUserFromTrip(int id,string uname)
        {
            
            var passangers = tripUserRepository.GetList(new TripUserById(id));
            foreach(TripUser tu in passangers)
            {
                var x = applicationUserRepository.GetById(tu.UserId);
                if (x.UserName == uname)
                {
                    tripUserRepository.RemoveUserFromTrip(id,x.Id);
                    break;
                }
            }
            
            return RedirectToAction("Index", "Home");
        }

    }
}
