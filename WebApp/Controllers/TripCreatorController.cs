using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;
using WebApp.Data;
using WebApp.Data.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class TripCreatorController : Controller
    {
        protected IAccountManager accountManager;
        protected ITripDetailsRepository tripDetailsRepository;
        private IImageSaver imageSaver;

        public TripCreatorController(
            IAccountManager _accountManager, 
            ITripDetailsRepository _tripDetailsRepository,
            IImageSaver _imageSaver)
        {
            accountManager = _accountManager;
            tripDetailsRepository = _tripDetailsRepository;
            imageSaver = _imageSaver;
        }
        /// <summary>
        /// Default HTTPGet 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Controls TripCreator form
        /// </summary>
        /// <param name="model">
        /// Data that user put while creating trip
        /// </param>
        /// <param name="answer">
        /// Hold information which button user used
        /// </param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public IActionResult Index(TripCreatorViewModel model,string answer)
        {
            if (!String.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Accept":
                        if (ModelState.IsValid && model.IsValid(model))
                        {
                            return View("ConfirmationPositive", model);
                        }
                        else
                        {
                            return View("ValidationError");
                        }
                    case "Decline":
                        return RedirectToAction("Index", "Home");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            else return View();
        }
        /// <summary>
        /// Controller for confirming created trip
        /// </summary>
        /// <param name="answer">
        /// Hold information which button user used
        /// </param>
        /// <param name="model">
        /// Data that user put while creating trip
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmationPositive(string answer, TripCreatorViewModel model)
        {
            if (!String.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Accept":
                        TripDetails tripDetails = model.GetTripDetailsModel();
                        tripDetails.DriverId = accountManager.GetUserId(HttpContext.User);

                        if (model.MapData != null)
                        {
                            var id = imageSaver.SaveImage(model.MapData, ".png", "wwwroot/images/maps/");
                            tripDetails.MapId = id;
                        }

                        tripDetailsRepository.Add(tripDetails);
                        return RedirectToAction("Index", "Home");
                    case "Decline":
                        return View("Index");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Controller for wrong data put by user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidationError()
        {
            return View("Index");
        }

    }
}