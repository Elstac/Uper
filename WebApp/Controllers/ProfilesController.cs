using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Repositories;
using WebApp.Models;
using WebApp.Data.Specifications;
using WebApp.Data;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    public class ProfilesController : Controller
    {
        private IAccountManager accountManager;
        private IApplicationUserViewModelGenerator generator;
        private IApplicationUserRepository repository;
        private ITripDetailsRepository tripDetailsRepository;
        private ITripUserRepository tripUserRepository;

        public ProfilesController(IApplicationUserViewModelGenerator generator, IAccountManager accountManager,IApplicationUserRepository repository,
            ITripDetailsRepository tripDetailsRepository, ITripUserRepository tripUserRepository)
        {
            this.generator = generator;
            this.accountManager = accountManager;
            this.repository = repository;
            this.tripDetailsRepository = tripDetailsRepository;
            this.tripUserRepository = tripUserRepository;
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

        [Authorize]
        public IActionResult TravelOffers()
        {
            return View("UserTravelOffersList", new List<TripDetails>());
        }

        [Authorize]
        public IActionResult MyTravelOffers()
        {
            List<TripDetails> myTravelOffers = tripDetailsRepository.GetList(new TripDetailsByDriverId(accountManager.GetUserId(HttpContext.User))).ToList(); ;
            for(int i =0;i<myTravelOffers.Count;i++)
            {
                if (myTravelOffers[i].DateEnd.CompareTo(DateTime.Now) <= 0) myTravelOffers.RemoveAt(i--);
            }
            return View("UserTravelOffersList", myTravelOffers);
        }

        [Authorize]
        public IActionResult MyFinishedTravelOffers()
        {
            List<TripDetails> myTravelOffers = tripDetailsRepository.GetList(new TripDetailsByDriverId(accountManager.GetUserId(HttpContext.User))).ToList(); ;
            for (int i = 0; i < myTravelOffers.Count; i++)
            {
                if (myTravelOffers[i].DateEnd.CompareTo(DateTime.Now) > 0) myTravelOffers.RemoveAt(i--);
            }
            return View("UserTravelOffersList", myTravelOffers);
        }

        [Authorize]
        public IActionResult JoinedTravelOffers()
        {
            var tripUser = tripUserRepository.GetList(new TripUserByUserId(accountManager.GetUserId(HttpContext.User)));
            List<TripDetails> joinedTravelOffers = new List<TripDetails>();
            TripDetails td;
            foreach(var tu in tripUser)
            {
                td = tripDetailsRepository.GetById(tu.TripId);
                if (td.DateEnd.CompareTo(DateTime.Now) > 0) joinedTravelOffers.Add(td);
            }


            return View("UserTravelOffersList", joinedTravelOffers);
        }

        [Authorize]
        public IActionResult JoinedFinishedTravelOffers()
        {
            var tripUser = tripUserRepository.GetList(new TripUserByUserId(accountManager.GetUserId(HttpContext.User)));
            List<TripDetails> joinedTravelOffers = new List<TripDetails>();
            TripDetails td;
            foreach (var tu in tripUser)
            {
                td = tripDetailsRepository.GetById(tu.TripId);
                if (td.DateEnd.CompareTo(DateTime.Now) <= 0) joinedTravelOffers.Add(td);
            }


            return View("UserTravelOffersList", joinedTravelOffers);
        }
    }
}