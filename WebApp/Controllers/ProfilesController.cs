using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Models;
using WebApp.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Entities;
using WebApp.ViewModels;
using WebApp.Models.TripDetailViewModelProvider;

namespace WebApp.Controllers
{
    public class ProfilesController : Controller
    {
        private IAccountManager accountManager;
        private IApplicationUserViewModelGenerator generator;
        private IApplicationUserRepository repository;
        private ITripDetailsRepository tripDetailsRepository;
        private ITripUserRepository tripUserRepository;
        private IRatesAndCommentRepository ratesAndCommentRepository;
        private ITripDetailsViewModelConverter tripDetailsConverter;

        public ProfilesController(
            IApplicationUserViewModelGenerator generator,
            IAccountManager accountManager,
            IApplicationUserRepository repository,
            ITripDetailsRepository tripDetailsRepository, 
            ITripUserRepository tripUserRepository, 
            IRatesAndCommentRepository ratesAndCommentRepository,
            ITripDetailsViewModelConverter tripDetailsConverter)
        {
            this.generator = generator;
            this.accountManager = accountManager;
            this.repository = repository;
            this.tripDetailsRepository = tripDetailsRepository;
            this.tripUserRepository = tripUserRepository;
            this.ratesAndCommentRepository = ratesAndCommentRepository;
            this.tripDetailsConverter = tripDetailsConverter;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ProfileEdit()
        {
            return View(new ProfileEditViewModel(""));
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProfileEdit(string Name,string Surname,string Description,string OldPassword,string NewPassword,string AnsString)
        {
            if (AnsString == "Cancel")
                return View(new ProfileEditViewModel("Cancel"));

            if (OldPassword == null && NewPassword != null || OldPassword != null && NewPassword == null)
                return View(new ProfileEditViewModel("EmptyPassword"));

            var user = repository.GetById(accountManager.GetUserId(User));
            
            if (OldPassword != null && NewPassword != null)
                if (!accountManager.CheckPassword(user, OldPassword))
                    return View(new ProfileEditViewModel("WrongPassword"));

            if (OldPassword == NewPassword && OldPassword != null && NewPassword != null)
                return View(new ProfileEditViewModel("SamePassword"));
        
            if (AnsString == "Confirm")
            {
                if (OldPassword != null && NewPassword != null)
                {
                    if (accountManager.CheckPassword(user, OldPassword))
                    {
                        var ValidationResult = accountManager.ValidatePassword(user, NewPassword);
                        if (ValidationResult.Succeeded)
                            accountManager.ChangePassword(user, OldPassword, NewPassword);
                        else return View(new ProfileEditViewModel("ValidationError", ValidationResult.Errors));                        
                    }                     
                        
                }

                if (Name != null)
                    user.Name = Name;
                if (Surname != null)
                    user.Surname = Surname;
                if (Description != null)
                    user.Description = Description;
                repository.Update(user);
                return View(new ProfileEditViewModel("Confirm"));
            }
            return View(new ProfileEditViewModel(""));
        }

        [Authorize]
        public IActionResult TravelOffers()
        {
            return View("UserTravelOffersList", new List<TripDetails>());
        }

        [Authorize]
        public IActionResult MyTravelOffers()
        {
            return View(
                "UserTravelOffersList",
                 tripDetailsConverter.Convert(
                     tripDetailsRepository.GetList(new NotFinishedDriversTrips(accountManager.GetUserId(HttpContext.User))).ToList(),
                     ViewerType.Driver
                     )
                 );
        }

        [Authorize]
        public IActionResult MyFinishedTravelOffers()
        {
            return View(
                    "UserTravelOffersList",
                    tripDetailsConverter.Convert(
                        tripDetailsRepository.GetList(new FinishedDriversTrips(accountManager.GetUserId(HttpContext.User))).ToList()
                        , ViewerType.Driver
                        )
                );
        }

        [Authorize]
        public IActionResult JoinedTravelOffers()
        {
            return View(
                "UserTravelOffersList",
                tripDetailsConverter.Convert(
                    tripDetailsRepository.GetList(new NotFinishedJoinedUsersTrips(accountManager.GetUserId(HttpContext.User))).ToList()
                    , ViewerType.Passanger
                    )
                );
        }

        [Authorize]
        public IActionResult JoinedFinishedTravelOffers()
        {
            return View(
                "UserTravelOffersList",
                tripDetailsConverter.Convert(
                    tripDetailsRepository.GetList(new FinishedJoinedUsersTrips(accountManager.GetUserId(HttpContext.User))).ToList()
                    , ViewerType.Passanger
                    )
                );
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public IActionResult RateAndComment(string driverId,int tripId)
        {
            List<RatesAndComment> entry = ratesAndCommentRepository.GetList(new RatesAndCommentByTripIdAndUserId(accountManager.GetUserId(HttpContext.User), tripId)).ToList();
            TripUser x = tripUserRepository.GetById((tripId, accountManager.GetUserId(HttpContext.User)));
            
            if (entry.Count == 0)
            {
                if (x.Accepted)
                {
                    ViewBag.driverId = driverId;
                    ViewBag.tripId = tripId;
                    return View("RatesAndComment");
                }else
                {
                    return Content("You weren't passenger in this trip!!!");
                }
            }

            return Content("You have already rated and commented this profile. You can rate and comment driver profile only once after every trip.");
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public IActionResult RatesAndComment(RatesAndCommentViewModel ratesAndComment, string answer)
        {
            if (!String.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Accept":
                        if (ModelState.IsValid && 
                            ratesAndCommentRepository.GetList(new RatesAndCommentByTripIdAndUserId(accountManager.GetUserId(HttpContext.User), ratesAndComment.TripId)).ToList().Count == 0)
                        {
                            RatesAndComment rac = new RatesAndComment
                            {
                                PersonalCulture = ratesAndComment.PersonalCulture,
                                DrivingSafety = ratesAndComment.DrivingSafety,
                                Punctuality = ratesAndComment.Punctuality,
                                Comment = ratesAndComment.Comment,
                                Date = DateTime.Now,
                                DriverId = ratesAndComment.DriverId,
                                UserId = accountManager.GetUserId(HttpContext.User),
                                TripId = ratesAndComment.TripId
                            };

                            ratesAndCommentRepository.Add(rac);
                            return RedirectToAction("DriverProfile",new { ratesAndComment.DriverId });
                        }
                        else return Content("Error with rates. Please try again!");
                case "Decline":
                        return RedirectToAction("Index", "Home");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
            
    


        [Authorize]
        public IActionResult DriverProfile(string driverId,string driverUserName)
        {
            if (driverId == null && driverUserName != null)
            {
                driverId = repository.GetUserIdByUserName(driverUserName);
            }
            else
            {
                driverId = accountManager.GetUserId(HttpContext.User);
            }

            var rates = ratesAndCommentRepository.GetList(new RatesAndCommentByDriverId(driverId)).ToList();

            DriverProfileViewModel model = new DriverProfileViewModel
            {
            ApplicationUserViewModel = generator.ConvertAppUserToViewModel(repository.GetById(driverId)),
            };
            model.SetListOfRatesAndComments(rates,repository);
            model.SetAverages();

            ViewBag.UserName = accountManager.GetUserName(HttpContext.User);
            return View("DriverProfile",model);
        }
    }
}