using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class TripCreatorController : Controller
    {
        /// <summary>
        /// Default HTTPGet 
        /// </summary>
        /// <returns></returns>
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
        [HttpPost]
        public IActionResult Index(TripCreatorViewModel model,string answer)
        {
            if (!String.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Accept":
                        if (ModelState.IsValid && model.IsCostValid(model.Cost))
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
        [HttpPost]
        public IActionResult ConfirmationPositive(string answer, TripCreatorViewModel model)
        {
            if (!String.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Accept":
                        model.ToDataBase();
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
        [HttpPost]
        public IActionResult ValidationError()
        {
            return View("Index");
        }

    }
}