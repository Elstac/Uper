using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;
using WebApp.Middlewares;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ChatController : Controller
    {
        private IAccountManager accountManager;
        private IChatEntryRepository chatEntryRepository;
        public ChatController(IAccountManager accountManager, IChatEntryRepository chatEntryRepository)
        {
            this.accountManager = accountManager;
            this.chatEntryRepository = chatEntryRepository;
        }

        [Authorize(Policy = "DriverAndPassangerOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int tripId)
        {
            ViewBag.UserName = accountManager.GetUserName(HttpContext.User);
            ViewBag.TripId = tripId;
            var x = chatEntryRepository.GetList(new ChatEntryByTripId(tripId));
            return View(x);
        }
    }
}